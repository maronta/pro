using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using Dapper;

namespace BuyListCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO:本番ではコメントアウトを取る
            //txtFilePath.Text = Path.GetDirectoryName(Application.ExecutablePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 結果欄をクリア
            listViewResult.Items.Clear();
            txtResult.Clear();

            AccessMdb db = new AccessMdb();
            DataTable tb;
            // DBに接続できない場合
            if (!db.Connect(txtFilePath.Text + "\\data\\protra.mdb", -1))
            {
                MessageBox.Show("DBに接続できません");
                return;
            }
            
            // EndUpdate時にをまとめて描画
            listViewResult.BeginUpdate();
            
            for (int i = 0; i < chxSystems.Items.Count; i++)
            {
                // チェックオフの場合、次へ
                if(!chxSystems.GetItemChecked(i))
                {
                    continue;
                }
                String[] systemId = chxSystems.Items[i].ToString().Split(':');
                String sql = CreateSql(systemId[0].Trim(), dtpCalendar.Text);
                //tb = db.ExecuteSql(sql, -1);
                
                IEnumerable<DOrder> orderList = null;
                //orderList = db.ExecuteSql<DOrder>(sql, -1);
                orderList = db._conn.Query<DOrder>(sql);


                for (int j = 0; j < orderList.ToList<DOrder>().Count(); j++)
                {
                    DOrder dorder = orderList.ToList<DOrder>()[j];
                    
                    // 「買い」のみ編集する
                    if (dorder.OrderType == 1) continue;

                    // リストビューを編集
                    ListViewItem lvi = new ListViewItem(new string[] {
                        dorder.OrderDate.ToString("yyyy/MM/dd"),
                        dorder.MarketNM,
                        dorder.Code.ToString(),
                        dorder.BrandNM,
                        dorder.Price.ToString("#,0"),
                        dorder.OrderNumber.ToString("#,0"),
                        dorder.OrderType == 1 ? "買い" : "売り"
                    });

                    //ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
                    
                    listViewResult.Items.Add(lvi);

                    // テキストボックスを編集
                    txtResult.Text += systemId[0].Trim() + "\r\n"
                                    + dorder.MarketNM + "\r\n"
                                    + dorder.OrderDate.ToString("yy/MM/dd")
                                    + "," + dorder.Code.ToString()
                                    + "," + dorder.BrandNM
                                    + "," + dorder.Price.ToString()
                                    + "," + dorder.OrderNumber.ToString()
                                    + "," + (dorder.OrderType == 1 ? "買い" : "売り")
                                    + "\r\n";

                }
            }
            // リストをまとめて描画
            listViewResult.EndUpdate();

            db.Disconnect();
        }

        /// <summary>
        /// システムファイル取得
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetSystem_Click(object sender, EventArgs e)
        {
            //AccessMdb db = new AccessMdb();
            //DataTable tb;
            //// DBに接続できない場合
            //if (!db.Connect(txtFilePath.Text + @"\protra.mdb", -1))
            //{
            //    MessageBox.Show("DBに接続できません");
            //    return;
            //}
            
            //tb = db.ExecuteSql("select * from System ORDER BY @File ASC", -1);

            //for (int i = 0; i < tb.Rows.Count; i++)
            //{
            //    chxSystems.Items.Add(tb.Rows[i]["@Id"].ToString().PadLeft(3)
            //        + ":" + tb.Rows[i]["@File"].ToString(), true);
            //}
            
            //db.Disconnect();

            chxSystems.Items.Clear();
            String strDirPath = txtFilePath.Text + "\\system";

            if (Directory.Exists(strDirPath))
            {
                String[] fileList = Directory.GetFiles(strDirPath, "*.pt");
                foreach (String s in fileList)
                {
                    chxSystems.Items.Add(Path.GetFileNameWithoutExtension(s));
                }

            }

        }

        /// <summary>
        /// MDBファイルを指定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            //OpenFileDialog ofd = new OpenFileDialog();
            ////ofd.FileName = "data.mdb";
            
            //// ダイアログを表示する
            //if (ofd.ShowDialog() == DialogResult.OK)
            //{
            //    // OKボタンがクリックされた時
            //    txtFilePath.Text = ofd.FileName;
            //}

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            //上部に表示する説明テキストを指定する
            fbd.Description = "フォルダを指定してください。";

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                txtFilePath.Text = fbd.SelectedPath;
            }
        }

        /// <summary>
        /// 検索クエリ作成
        /// </summary>
        /// <param name="filename">ファイル名</param>
        /// <param name="date">日付</param>
        /// <returns></returns>
        private String CreateSql(String filename, String date)
        {
            String sql = "";

            sql = "SELECT @SystemId AS SystemId, Market.@Id AS MarketId, Market.@Name AS MarketNM,";
            sql += "      @BrandId AS BrandId, Brand.@Code AS Code, Brand.@Name AS BrandNM,";
            sql += "      @Date AS OrderDate, @Price AS Price, @Number AS OrderNumber, @Order AS OrderType";
            sql += " FROM ((SystemLog INNER JOIN System ON SystemLog.@SystemId = System.@Id)";
            sql += "        INNER JOIN Brand ON SystemLog.@BrandId = Brand.@Id)";
            sql += "        INNER JOIN Market ON Brand.@MarketId = Market.@Id";
            sql += " WHERE @File = " + "\"" + filename + ".pt\"";
            sql += " AND @Date = " + "#" + date + "#";
            sql += " AND @Order = 0";
            return sql;
        }

        /// <summary>
        /// [すべて選択]ボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllCheckOn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chxSystems.Items.Count; i++)
            {
                chxSystems.SetItemChecked(i, true);
            }
        }
        /// <summary>
        /// [すべて解除]ボタンをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllCheckOff_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < chxSystems.Items.Count; i++)
            {
                chxSystems.SetItemChecked(i, false);
            }
        }
    }
}
