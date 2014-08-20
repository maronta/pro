using System;
using System.Collections;
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
            listViewResult.Items.Clear();
            //TODO:本番ではコメントアウトを取る
            txtFilePath.Text = Path.GetDirectoryName(Application.ExecutablePath);
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

            txtResult.Text += dtpCalendar.Text + Environment.NewLine;

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

                Markets mks = new Markets();

                for (int j = 0; j < orderList.ToList<DOrder>().Count(); j++)
                {
                    if (j == 0)
                    {
                        txtResult.Text += Environment.NewLine + systemId[0].Trim() + Environment.NewLine;
                    }

                    DOrder dorder = orderList.ToList<DOrder>()[j];
                    
                    // 「売り」は飛ばす
                    if (dorder.OrderType == 1) continue;

                    // リストビューを編集
                    ListViewItem lvi = new ListViewItem(new string[] {
                        dorder.OrderDate.ToString("yyyy/MM/dd"),
                        dorder.MarketNM,
                        dorder.Code.ToString(),
                        dorder.BrandNM,
                        dorder.Price.ToString("#,0"),
                        dorder.OrderNumber.ToString("#,0"),
                        dorder.OrderType == 0 ? "買" : "売"
                    });

                    //ListViewItem.ListViewSubItem lvsi = new ListViewItem.ListViewSubItem();
                    
                    listViewResult.Items.Add(lvi);

                    // 対象の市場名がまだ表示されていない場合
                    if (!mks.hasDisplayed[dorder.MarketId])
                    {
                        mks.hasDisplayed[dorder.MarketId] = true;
                        txtResult.Text += dorder.MarketNM + Environment.NewLine;
                    }

                    // テキストボックスを編集
                    txtResult.Text += dorder.Code.ToString()
                                    + "," + dorder.BrandNM
                                    + "," + dorder.Price.ToString()
                                    + "," + dorder.OrderNumber.ToString()
                                    + "," + (dorder.OrderType == 0 ? "買" : "売")
                                    + Environment.NewLine;

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
            sql += " ORDER BY @FIle, Market.@Id";

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

        /// <summary>
        /// [Result]テキストボックスでキーダウン時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtResult_KeyDown(object sender, KeyEventArgs e)
        {
            // [Ctrl+a]でテキストを全選択する
            if (e.Control && e.KeyCode == Keys.A)
            {
                txtResult.SelectAll();
            }
        }
    }

    public class Markets
    {
        public List<Boolean> hasDisplayed;

        public Markets()
        {
            bool[] dis = new bool[11];
            hasDisplayed = new List<Boolean>(dis);
        }
    }
}
