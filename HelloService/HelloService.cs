using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Configuration;
using System.ServiceModel.Web;
using System.Net;


namespace HelloService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HelloService" in both code and config file together.
    public class HelloService : IHelloService
    {


        string connectionString = ConfigurationManager.ConnectionStrings["Genius_ConnectionString"].ConnectionString;
        SqlConnection conn;
        SqlCommand comm;

        public string ProcessData(string firstname, string lastname, string Mobile, string monthlypayment)
        {

            conn = new SqlConnection(connectionString);
            conn.Open();
            comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandType = System.Data.CommandType.StoredProcedure;
            comm.CommandText = "addSeedWebleads";
            comm.Parameters.AddWithValue("@Firstname", firstname);
            comm.Parameters.AddWithValue("@Surname", lastname);
            comm.Parameters.AddWithValue("@Tel1", Mobile);
            comm.Parameters.AddWithValue("@MonthlyPayment", monthlypayment);
            comm.Parameters["@MonthlyPayment"].Direction = ParameterDirection.Output;


            try
            {
                comm.ExecuteNonQuery();
                string retunvalue = comm.Parameters["@MonthlyPayment"].Value.ToString();
                return retunvalue;
            }
            catch (Exception ex)
            {
                return "Not Saved" + ex;
            }
            finally
            {
                conn.Close();
            }
        }



        public string GetMessage(string firstname, string lastname, string Mobile)
        {

            SqlConnection lSQLConn = null;
            SqlCommand lSQLCmd = new SqlCommand();
            string lsResponse = "";
            string connStr = "";

            connStr = ConfigurationManager.ConnectionStrings["Genius_ConnectionString"].ConnectionString;

            try
            {
                lSQLConn = new SqlConnection(connStr);
                lSQLConn.Open();
                lSQLCmd.CommandType = CommandType.StoredProcedure;
                lSQLCmd.CommandText = ConfigurationManager.AppSettings["Lead_SP"].ToString();
                lSQLCmd.Parameters.Add(new SqlParameter("FirstName", firstname));
                lSQLCmd.Parameters.Add(new SqlParameter("surname", lastname));
                lSQLCmd.Parameters.Add(new SqlParameter("tel1", Mobile));


                lSQLCmd.Connection = lSQLConn;
                lSQLCmd.ExecuteNonQuery();
                // lsResponse = Convert.ToString(lSQLCmd.ExecuteScalar());

                lsResponse = lSQLCmd.Parameters["@leadSourceID"].Value.ToString();
            }
            catch (Exception Exc)
            {
                return "Error: " + Exc.Message;
            }
            finally
            {
                lSQLCmd.Dispose();
                lSQLConn.Close();
            }

            if (String.IsNullOrEmpty(lsResponse))
            {
                return "Error: Unspecified problem while adding task.";
            }
            return lsResponse;
        }

        public bool UrlIsValid(string url)
        {

            Uri uri = null;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri) || null == uri)
            {
                //Invalid URL
                return false;
            }
            else
            {
                return true;
            }
            //string error;
            //try
            //{
            //    HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            //    request.Timeout = 5000; //set the timeout to 5 seconds to keep the user from waiting too long for the page to load
            //    request.Method = "HEAD"; //Get only the header information -- no need to download any content

            //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            //    int statusCode = (int)response.StatusCode;
            //    if (statusCode >= 100 && statusCode < 400) //Good requests
            //   // if (statusCode != 0)
            //    {
            //        //here goes the call to the database to check the website is Allowed or not using url value

            //        return true;

            //    }
            //    else if (statusCode >= 500 && statusCode <= 510) //Server Errors
            //    {
            //        //  log.Warn(String.Format("The remote server has thrown an internal error. Url is not valid: {0}", url));
            //        error = "The remote server has thrown an internal error. Url is not valid: {0}" + url;
            //        return false;
            //    }
            //}
            //catch (WebException ex)
            //{
            //    if (ex.Status == WebExceptionStatus.ProtocolError) //400 errors
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        // log.Warn(String.Format("Unhandled status [{0}] returned for url: {1}", ex.Status, url), ex);
            //        error = "Unhandled status [{0}] returned for url: {1}" + ex.Status + url + ex;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //  log.Error(String.Format("Could not test url {0}.", url), ex);
            //    error = "Could not test url {0}." + url + ex;
            //}
            //return false;
        }

        public string ProcessDataAdd(string firstname, string lastname, string email, string Mobile, string debtlevel, string monthlypayment, string website)
        {
            if (UrlIsValid(website) == true)
            {


                // Uri requestUri = System.ServiceModel.OperationContext.Current.RequestContext.RequestMessage.Headers.To;
                // var requestUri = System.ServiceModel.Web.WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.OriginalString;
                //  var requestUri = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
                //    string requestedUrl = WebOperationContext.Current.IncomingRequest.Headers[System.Net.HttpRequestHeader.Referer].ToString();

                if (string.IsNullOrEmpty(firstname) || string.IsNullOrEmpty(lastname) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(Mobile) || string.IsNullOrEmpty(debtlevel) || string.IsNullOrEmpty(monthlypayment))
                {
                    return "Please Submit Missing Data";
                }
                else
                {
                    conn = new SqlConnection(connectionString);
                    conn.Open();
                    comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = "addSeedWebleads";
                    comm.Parameters.AddWithValue("@Firstname", firstname);
                    comm.Parameters.AddWithValue("@Surname", lastname);
                    comm.Parameters.AddWithValue("@Email", email);
                    comm.Parameters.AddWithValue("@Tel1", Mobile);
                    comm.Parameters.AddWithValue("@DebtLevel", debtlevel);
                    comm.Parameters.AddWithValue("@MonthlyPayment", monthlypayment);
                    comm.Parameters.AddWithValue("@website", website);
                    comm.Parameters.Add(new SqlParameter("@ClientAccountNumber1", SqlDbType.VarChar, 100));
                    comm.Parameters["@ClientAccountNumber1"].Direction = ParameterDirection.Output;

                    try
                    {
                        comm.ExecuteNonQuery();
                        string retunvalue = comm.Parameters["@ClientAccountNumber1"].Value.ToString();

                        return "Record Saved." + " " + "Your Ref is : " + retunvalue;
                    }
                    catch (Exception ex)
                    {
                        return "Not Saved" + ex;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

            }
            else
            {
                return "Web Site is not Valid";

            }
        }
    }
}



           
            
            
            
         
