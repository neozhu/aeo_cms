using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oncontrol3.Web.Helpers
{
    public class FWOINPUTROOT
    {
        public string amt_gdsv_cur { get; set; } // 货物价值（货币）
        public string amt_gdsv_val { get; set; } // 货物价值
        public string base_btd_id { get; set; } // 基础业务交易凭证
        public string consignee_id { get; set; } // 收货方
        public string consignee_key { get; set; } // 收货方业务伙伴全局唯一标识符
        public string des_loc_id { get; set; } // 目标位置
        public string des_loc_key { get; set; } // 位置
        public string fag_item_key { get; set; } // 货运协议项目键值
        public string fag_key { get; set; } // 货运协议键值
        public string lifecycle { get; set; } // 生命周期状态
        public string movem_type { get; set; } // 移动类型
        public string order_party_id { get; set; } // 订购方
        public string order_party_key { get; set; } // 订购方业务伙伴全局唯一标识符
        public string pic_ear_req { get; set; } // 提货（自）
        public string sales_org_id { get; set; } // 销售组织
        public string service_product_id { get; set; } // 服务产品
        public string shipper_id { get; set; } // 发货方
        public string shipper_key { get; set; } // 发货方业务伙伴全局唯一标识符
        public string src_loc_id { get; set; } // 源位置
        public string src_loc_key { get; set; } // 位置
        public string transsrvlvl_code { get; set; } // 运输服务级别 - 销售
        public string trq_type { get; set; } // 凭证类型
        public string zbcbj { get; set; } // 本仓标记
        public string zchubcsj { get; set; } // 出hub仓时间
        public string zcjtk { get; set; } // 成交条款
        public string zfdh { get; set; } // 分单号
        public string zhdfyd { get; set; } // 货代分运单
        public string zhwdl { get; set; } // 海外代理
        public string zhydl { get; set; } // 货运代理
        public string zjdbzl { get; set; } // 接单备注栏
        public string zjhbz { get; set; } // 急货标记
        public string zjhhbrq { get; set; } // 计划航班日期
        public string zjhshthrq { get; set; } // 计划送货/提货日期
        public string zjhubcsj { get; set; } // 进hub仓时间
        public string zjsbzl { get; set; } // 结算备注栏
        public string zkhfphl { get; set; } // 客户发票号栏
        public string zkhzbh { get; set; } // 客户自编号
        public string zkjbj { get; set; } // 快件标记
        public string zmdg { get; set; } // 目的港
        public string zqyg { get; set; } // 起运港
        public string zshyqbzl { get; set; } // 送货要求备注栏
        public string ztgfs { get; set; } // 通关方式
        public string zthyqbzl { get; set; } // 提货要求备注栏
        public string zzdh { get; set; } // 总单号
    }
    public class ZROOTEXD1
    {
        public string zcgs { get; set; } // 船公司
        public string zcgsjsh { get; set; } // 船公司结算号（合约号）
        public string zcztg1 { get; set; } // 操作退关
        public string zdcyqbzl { get; set; } // 订舱要求备注栏
        public string zjh { get; set; } // 急货
        public string zjhshrq { get; set; } // 计划送货日期
        public string zjhthrqzgrq { get; set; } // 计划提货日期／做柜日期
        public string zkhfph { get; set; } // 客户发票号
        public string zkhjsh { get; set; } // 客户结算号
        public string zkhrq { get; set; } // 开航日期
        public string zkhsx { get; set; } // 客户属性
        public string zlh { get; set; } // 料号
        public string zlx { get; set; } // 落箱
        public string zmdg1hc { get; set; } // 目的国（仅海出）
        public string zmdghc { get; set; } // 目的港（仅海出）
        public string zmdhj { get; set; } // 门点（仅海进）
        public string zpcfs { get; set; } // 配舱方式
        public string zqtbzl { get; set; } // 其他备注栏
        public string zqydhc { get; set; } // 起运地（仅海出）
        public string zqyg { get; set; } // 起运港
        public string zqyg1 { get; set; } // 起运国
        public string ztdmdghc { get; set; } // 提单目的港（仅海出）
        public string ztyhbj { get; set; } // 退运货标记
        public string zxsy { get; set; } // 销售员
        public string zyccmhj { get; set; } // 一程船名（仅海进）
        public string zychchj { get; set; } // 一程航次（仅海进）
        public string zyctdhj { get; set; } // 一程提单（仅海进）
        public string zzlqq { get; set; } // 资料齐全
        public string zzlqqrq { get; set; } // 资料齐全日期
        public string zzygqhc { get; set; } // 装运港区（仅海出）
        public string zzzghc { get; set; } // 中转港（仅海出）
    }
    public class ZTD
    {
        public string zbgkzx { get; set; } // 报关可执行
        public string zdrtzr { get; set; } // 第二通知人
        public string zdrtzrjc { get; set; } // 第二通知人名称
        public string zfhr { get; set; } // 发货人
        public string zfhrjc { get; set; } // 发货人名称
        public string zhwms { get; set; } // 货物描述
        public string zkhzbh { get; set; } // 客户自编号
        public string zmt { get; set; } // 唛头
        public string zsbytd { get; set; } // 放箱用提单
        public string zshr { get; set; } // 收货人
        public string zshrjc { get; set; } // 收货人名称
        public string ztzr { get; set; } // 通知人
        public string ztzrjc { get; set; } // 通知人名称
    }
    public class PARTY
    {
        public string party_id { get; set; } // 业务伙伴内部标识
        public string party_rco { get; set; } // 当事方角色
        public string party_uuid { get; set; } // 业务伙伴
    }
    public class ITEM
    {
        public string item_id { get; set; } // 项目
        public string item_cat { get; set; } // 项目类别
        public string transsrvreq_code { get; set; } // 服务类型
        public string qua_pcs_val { get; set; } // 数量
        public string qua_pcs_uni { get; set; } // 数量计量单位
        public string base_uom_val { get; set; } // 基本数量
        public string base_uom_uni { get; set; } // 基本计量单位
        public string gro_wei_val { get; set; } // 毛重
        public string gro_wei_uni { get; set; } // 毛重计量单位
        public string gro_vol_val { get; set; } // 总体积
        public string gro_vol_uni { get; set; } // 总体积计量单位
        public string package_tco { get; set; } // 包装类型
        public string zgqsx { get; set; } // 关区属性
        public string zjckka { get; set; } // 进出口口岸
        public string zkaczrqhj { get; set; } // 口岸操作日期(仅海进）
        public string zmyfs1 { get; set; } // 贸易方式
        public string zsbfs { get; set; } // 申报方式
        public string zsbgq { get; set; } // 申报关区
        public string zwtfs { get; set; } // 委托方式
    }
    public class ZITEMEXD1
    {
        public string zbgfs { get; set; } // 报关方式
        public string zbgjydw { get; set; } // 报关经营单位
        public string zcgzzdm { get; set; } // 采购组织代码
        public string zexzyhtfph { get; set; } // 二线专用合同发票号
        public string zfwgfdm1 { get; set; } // 服务供方代码1
        public string zjhbgrq { get; set; } // 计划报关日期
        public string zmzbz { get; set; } // 木质包装
        public string zzhqdh { get; set; } // 载货清单号
    }

    public class ITEMCCODE
    {
        public string ccode_type { get; set; } // 商品代码类型
        public string ccode { get; set; } // 商品代码
        public string zzwpm { get; set; } // 中文品名
    }
}