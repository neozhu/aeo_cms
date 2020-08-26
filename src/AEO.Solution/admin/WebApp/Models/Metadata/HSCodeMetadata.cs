using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace WebApp.Models
{
// <copyright file="HSCodeMetadata.cs" tool="martCode MVC5 Scaffolder">
// Copyright (c) 2020 All Rights Reserved
// </copyright>
// <author>neo.zhu</author>
// <date>2020/8/26 11:32:52 </date>
// <summary>Class representing a Metadata entity </summary>
    //[MetadataType(typeof(HSCodeMetadata))]
    public partial class HSCode
    {
    }

    public partial class HSCodeMetadata
    {
        [Required(ErrorMessage = "Please enter : 系统主键")]
        [Display(Name = "Id",Description ="系统主键",Prompt = "系统主键",ResourceType = typeof(resource.HSCode))]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter : 10位HS编码")]
        [Display(Name = "hscode",Description ="10位HS编码",Prompt = "10位HS编码",ResourceType = typeof(resource.HSCode))]
        [MaxLength(10)]
        public string hscode { get; set; }

        [Display(Name = "cn_name",Description ="商品名称",Prompt = "商品名称",ResourceType = typeof(resource.HSCode))]
        [MaxLength(512)]
        public string cn_name { get; set; }

        [Display(Name = "en_name",Description ="商品英文名称",Prompt = "商品英文名称",ResourceType = typeof(resource.HSCode))]
        [MaxLength(256)]
        public string en_name { get; set; }

        [Display(Name = "g_model",Description ="申报要素",Prompt = "申报要素",ResourceType = typeof(resource.HSCode))]
        [MaxLength(256)]
        public string g_model { get; set; }

        [Display(Name = "unit_code",Description ="第一法定单位代码",Prompt = "第一法定单位代码",ResourceType = typeof(resource.HSCode))]
        [MaxLength(3)]
        public string unit_code { get; set; }

        [Display(Name = "unit_name",Description ="第一法定单位名称",Prompt = "第一法定单位名称",ResourceType = typeof(resource.HSCode))]
        [MaxLength(12)]
        public string unit_name { get; set; }

        [Display(Name = "unit2_code",Description ="第二法定单位代码",Prompt = "第二法定单位代码",ResourceType = typeof(resource.HSCode))]
        [MaxLength(3)]
        public string unit2_code { get; set; }

        [Display(Name = "unit2_name",Description ="第二法定单位名称",Prompt = "第二法定单位名称",ResourceType = typeof(resource.HSCode))]
        [MaxLength(12)]
        public string unit2_name { get; set; }

        [Display(Name = "control_ma",Description ="监管条件代码",Prompt = "监管条件代码",ResourceType = typeof(resource.HSCode))]
        [MaxLength(256)]
        public string control_ma { get; set; }

        [Display(Name = "ciq_ma",Description ="检验检疫类别代码",Prompt = "检验检疫类别代码",ResourceType = typeof(resource.HSCode))]
        [MaxLength(256)]
        public string ciq_ma { get; set; }

        [Required(ErrorMessage = "Please enter : 进口最惠国税率")]
        [Display(Name = "im_low_rate",Description ="进口最惠国税率",Prompt = "进口最惠国税率",ResourceType = typeof(resource.HSCode))]
        public decimal im_low_rate { get; set; }

        [Required(ErrorMessage = "Please enter : 进口普通税率")]
        [Display(Name = "im_normal_rate",Description ="进口普通税率",Prompt = "进口普通税率",ResourceType = typeof(resource.HSCode))]
        public decimal im_normal_rate { get; set; }

        [Required(ErrorMessage = "Please enter : 进口暂定税率")]
        [Display(Name = "im_temp_rate",Description ="进口暂定税率",Prompt = "进口暂定税率",ResourceType = typeof(resource.HSCode))]
        public decimal im_temp_rate { get; set; }

        [Required(ErrorMessage = "Please enter : 增值税税率")]
        [Display(Name = "im_tax_rate",Description ="增值税税率",Prompt = "增值税税率",ResourceType = typeof(resource.HSCode))]
        public decimal im_tax_rate { get; set; }

        [Required(ErrorMessage = "Please enter : 进口消费税税率")]
        [Display(Name = "im_consume_rate",Description ="进口消费税税率",Prompt = "进口消费税税率",ResourceType = typeof(resource.HSCode))]
        public decimal im_consume_rate { get; set; }

        [Display(Name = "ex_return_rate",Description ="进口消费税税率",Prompt = "进口消费税税率",ResourceType = typeof(resource.HSCode))]
        public decimal ex_return_rate { get; set; }

        [Display(Name = "ex_normal_rate",Description ="出口普通税率",Prompt = "出口普通税率",ResourceType = typeof(resource.HSCode))]
        public decimal ex_normal_rate { get; set; }

        [Display(Name = "ex_temp_rate",Description ="出口暂定税率",Prompt = "出口暂定税率",ResourceType = typeof(resource.HSCode))]
        public decimal ex_temp_rate { get; set; }

        [Display(Name = "ex_special_rate",Description ="出口特殊税税率",Prompt = "出口特殊税税率",ResourceType = typeof(resource.HSCode))]
        public decimal ex_special_rate { get; set; }

        [Display(Name = "ex_tax_rate",Description ="出口增值税税率",Prompt = "出口增值税税率",ResourceType = typeof(resource.HSCode))]
        public decimal ex_tax_rate { get; set; }

        [Display(Name = "remark",Description ="商品备注",Prompt = "商品备注",ResourceType = typeof(resource.HSCode))]
        [MaxLength(512)]
        public string remark { get; set; }

        [Display(Name = "CreatedDate",Description ="创建时间",Prompt = "创建时间",ResourceType = typeof(resource.HSCode))]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "CreatedBy",Description ="创建用户",Prompt = "创建用户",ResourceType = typeof(resource.HSCode))]
        [MaxLength(20)]
        public string CreatedBy { get; set; }

        [Display(Name = "LastModifiedDate",Description ="最后更新时间",Prompt = "最后更新时间",ResourceType = typeof(resource.HSCode))]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "LastModifiedBy",Description ="最后更新用户",Prompt = "最后更新用户",ResourceType = typeof(resource.HSCode))]
        [MaxLength(20)]
        public string LastModifiedBy { get; set; }

        [Required(ErrorMessage = "Please enter : 租户主键")]
        [Display(Name = "TenantId",Description ="租户主键",Prompt = "租户主键",ResourceType = typeof(resource.HSCode))]
        public int TenantId { get; set; }

    }

}
