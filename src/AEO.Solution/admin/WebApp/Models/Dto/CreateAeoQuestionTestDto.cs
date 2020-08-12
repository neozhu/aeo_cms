using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models.Dto
{
  public class CreateAeoQuestionTestDto
  {


    public string Name { get; set; }

    public string TradeCode { get; set; }


    public string CreditCode { get; set; }


    public string Ctype { get; set; }

    public string TestNo { get; set; }

    public string AuthType { get; set; }

    public string MasterCustom { get; set; }

    public DateTime? RegistDate { get; set; }

    public string IsForeign { get; set; }

    public string Zone { get; set; }

    public decimal? RegistedTime { get; set; }

    public string Unit { get; set; }

    public DateTime? AuthDate { get; set; }

    public string Tester { get; set; }

    public int? Year { get; set; }


    public DateTime? BeginDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string Remark { get; set; }


    public string Status { get; set; }

    public decimal? StdScore { get; set; }

    public decimal? Score { get; set; }

    public string Result { get; set; }

    public IEnumerable<Answer> Answers { get; set; }
  }

  public class Answer
  {
    public int tplId { get; set; }
    public int Score { get; set; }
    }
}