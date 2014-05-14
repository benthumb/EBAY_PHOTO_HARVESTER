using System;
using MySql.Data.MySqlClient;
using NLog;

namespace EBAY_PHOTO_HARVESTER
{
  class DataStoreConnectivity
  {
    private static readonly Logger _logger = LogManager.GetLogger("EBAY_PHOTO_HARVESTER.DataStoreConnectivity");
    public void GetMySQLConnection()
    {
      string cs = @"server=localhost;userid=root;
            password=yew9ceja;database=ebay_images";

      MySqlConnection conn = null;

      try
      {
        _logger.Info("Trying to connect...");
        conn = new MySqlConnection(cs);
        conn.Open();
        //Console.WriteLine("MySQL version : {0}", conn.ServerVersion);
        _logger.Info("MySQL version : {0}", conn.ServerVersion);

      }
      catch (MySqlException ex)
      {
        _logger.Error("Database connection routine failed: " + ex.GetBaseException());
        _logger.Error("Error: {0}", ex.ToString());
        //Console.WriteLine("Error: {0}", ex.ToString());

      }
      finally
      {
        if (conn != null)
        {
          conn.Close();
          _logger.Info("All done--connection closed...");
        }
      }
    }
  }
}