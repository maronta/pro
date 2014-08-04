using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.OleDb;
using System.Data;

namespace BuyListCreator
{
    class AccessMdb
    {
        /// <summary>
        /// SQLコネクション
        /// </summary>
        public OleDbConnection _conn = null;
        /// <summary>
        /// トランザクションオブジェクト
        /// </summary>
        private OleDbTransaction _trn = null;
        
        /// <summary>
        /// DB接続
        /// </summary>
        /// <param name="dsn">データソース名</param>
        /// <param name="tot">タイムアウト値</param>
        public Boolean Connect(String dsn, int tot)
        {
            try
            {
                if (_conn == null)
                {
                    _conn = new OleDbConnection();
                }

                String cst = "";
                cst = cst + "Provider=Microsoft.Jet.OLEDB.4.0";
                cst = cst + ";Data Source=" + dsn;
                // データベースパスワードが設定されている場合
                // cst = cst + ";Jet OLEDB:Database Password=xxxxx";
                if (tot > -1)
                {
                    //_con.ConnectionTimeout = tot;
                    cst = cst + ";Connect Timeout=" + tot.ToString();
                }
                _conn.ConnectionString = cst;

                _conn.Open();

                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception("Connect Error", ex);
                Console.WriteLine(ex);
                return false;
            }
        }
        /// <summary>
        /// DB切断
        /// </summary>
        public void Disconnect()
        {
            try
            {
                _conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Disconnect Error", ex);
            }
        }
        /// <summary>
        /// SQLの実行
        /// </summary>
        /// <param name="sql">SQL文</param>
        /// <param name="tot">タイムアウト値</param>
        /// <returns></returns>
        public DataTable ExecuteSql(String sql, int tot)
        {
            DataTable dt = new DataTable();

            try
            {
                OleDbCommand sqlCommand = new OleDbCommand(sql, _conn, _trn);

                if (tot > -1)
                {
                    sqlCommand.CommandTimeout = tot;
                }
                OleDbDataAdapter adapter = new OleDbDataAdapter(sqlCommand);

                adapter.Fill(dt);
                adapter.Dispose();
                sqlCommand.Dispose();
            }
            catch (InvalidOperationException ex)
            {
                throw new Exception("ExecuteSql Error", ex);
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                throw new Exception("ExecuteSql Error", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("ExecuteSql Error", ex);
            }

            return dt;
        }

        /// <summary>
        /// トランザクション開始
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                _trn = _conn.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("BeginTransaction Error", ex);
            }
        }
        /// <summary>
        /// コミット
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (_trn != null)
                {
                    _trn.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CommitTransaction Error", ex);
            }
            finally
            {
                _trn = null;
            }
        }
        /// <summary>
        /// ロールバック
        /// </summary>
        public void RollbackTransaction()
        {
            try
            {
                if (_trn != null)
                {
                    _trn.Rollback();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("RollbackTransaction Error", ex);
            }
            finally
            {
                _trn = null;
            }
        }
        /// <summary>
        /// デストラクタ
        /// </summary>
        /// <remarks></remarks>
        ~AccessMdb()
        {
            Disconnect();
        }

        internal IEnumerable<DOrder> ExecuteSql<T1>(string sql, int p)
        {
            throw new NotImplementedException();
        }
    }
}
