using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
  public static class KeyGenerator
  {
    private static readonly SqlSugar.ISqlSugarClient _db = SqlSugarFactory.CreateSqlSugarClient();
   //获取自定义流水号
    public static string GetNextCustomsKey()
    {
      //通过MS SQL Sequence产生递增序列
      var result = _db.Ado.GetInt("SELECT NEXT VALUE FOR [dbo].[NoSequence]");
      return Convert.ToInt32(result).ToString("00000000");


    }
    //获取产品编号
    public static string GetProductKey()
    {
      var prefix = DateTime.Now.ToString("yyyyMM00");

      var result = _db.Ado.GetInt("exec [dbo].[SP_NextVal]  @prefix", new {prefix= prefix });
      return prefix+Convert.ToInt32(result).ToString("0000");


    }
    //获取客户编号
    public static string GetCustomerCode()
    {
      var prefix ="C" + DateTime.Now.ToString("yyyyMM");
      var result = _db.Ado.GetInt("exec [dbo].[SP_NextVal]  @prefix", new { prefix = prefix });
      return prefix + Convert.ToInt32(result).ToString("000");
    }
  }
}