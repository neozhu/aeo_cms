//-------账号类型---------//
var accounttypefiltersource = [{ value: '', text: 'All'}];
var accounttypedatasource = [];
accounttypefiltersource.push({ value: '0',text:'公司'  });
accounttypedatasource.push({ value: '0',text:'公司'  });
accounttypefiltersource.push({ value: '1',text:'供应商'  });
accounttypedatasource.push({ value: '1',text:'供应商'  });
accounttypefiltersource.push({ value: '2',text:'客户'  });
accounttypedatasource.push({ value: '2',text:'客户'  });
accounttypefiltersource.push({ value: '3',text:'外协单位'  });
accounttypedatasource.push({ value: '3',text:'外协单位'  });
//for datagrid AccountType field  formatter
function accounttypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = accounttypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = accounttypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   AccountType  field filter 
$.extend($.fn.datagrid.defaults.filters, {
accounttypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: accounttypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   AccountType   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
accounttypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: accounttypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------活动状态---------//
var actstatusfiltersource = [{ value: '', text: 'All'}];
var actstatusdatasource = [];
actstatusfiltersource.push({ value: '取消',text:'取消'  });
actstatusdatasource.push({ value: '取消',text:'取消'  });
actstatusfiltersource.push({ value: '完成',text:'完成'  });
actstatusdatasource.push({ value: '完成',text:'完成'  });
actstatusfiltersource.push({ value: '计划',text:'计划'  });
actstatusdatasource.push({ value: '计划',text:'计划'  });
actstatusfiltersource.push({ value: '进行中',text:'进行中'  });
actstatusdatasource.push({ value: '进行中',text:'进行中'  });
//for datagrid actstatus field  formatter
function actstatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = actstatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = actstatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   actstatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
actstatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: actstatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   actstatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
actstatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: actstatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------活动类型---------//
var acttypefiltersource = [{ value: '', text: 'All'}];
var acttypedatasource = [];
acttypefiltersource.push({ value: '促销活动',text:'促销活动'  });
acttypedatasource.push({ value: '促销活动',text:'促销活动'  });
acttypefiltersource.push({ value: '其他',text:'其他'  });
acttypedatasource.push({ value: '其他',text:'其他'  });
acttypefiltersource.push({ value: '合作伙伴',text:'合作伙伴'  });
acttypedatasource.push({ value: '合作伙伴',text:'合作伙伴'  });
acttypefiltersource.push({ value: '培训会',text:'培训会'  });
acttypedatasource.push({ value: '培训会',text:'培训会'  });
acttypefiltersource.push({ value: '展览',text:'展览'  });
acttypedatasource.push({ value: '展览',text:'展览'  });
acttypefiltersource.push({ value: '广告/简讯',text:'广告/简讯'  });
acttypedatasource.push({ value: '广告/简讯',text:'广告/简讯'  });
acttypefiltersource.push({ value: '研讨会/活动',text:'研讨会/活动'  });
acttypedatasource.push({ value: '研讨会/活动',text:'研讨会/活动'  });
//for datagrid acttype field  formatter
function acttypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = acttypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = acttypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   acttype  field filter 
$.extend($.fn.datagrid.defaults.filters, {
acttypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: acttypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   acttype   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
acttypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: acttypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------题目类别---------//
var authcategoryfiltersource = [{ value: '', text: 'All'}];
var authcategorydatasource = [];
authcategoryfiltersource.push({ value: '信息系统控制',text:'信息系统控制'  });
authcategorydatasource.push({ value: '信息系统控制',text:'信息系统控制'  });
authcategoryfiltersource.push({ value: '内部审计控制',text:'内部审计控制'  });
authcategorydatasource.push({ value: '内部审计控制',text:'内部审计控制'  });
authcategoryfiltersource.push({ value: '组织机构控制',text:'组织机构控制'  });
authcategorydatasource.push({ value: '组织机构控制',text:'组织机构控制'  });
authcategoryfiltersource.push({ value: '财务状况标准',text:'财务状况标准'  });
authcategorydatasource.push({ value: '财务状况标准',text:'财务状况标准'  });
authcategoryfiltersource.push({ value: '贸易安全标准',text:'贸易安全标准'  });
authcategorydatasource.push({ value: '贸易安全标准',text:'贸易安全标准'  });
authcategoryfiltersource.push({ value: '进出口业务控制',text:'进出口业务控制'  });
authcategorydatasource.push({ value: '进出口业务控制',text:'进出口业务控制'  });
authcategoryfiltersource.push({ value: '进出口业务规范',text:'进出口业务规范'  });
authcategorydatasource.push({ value: '进出口业务规范',text:'进出口业务规范'  });
authcategoryfiltersource.push({ value: '遵守法律法规',text:'遵守法律法规'  });
authcategorydatasource.push({ value: '遵守法律法规',text:'遵守法律法规'  });
//for datagrid authcategory field  formatter
function authcategoryformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = authcategorydatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = authcategorydatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   authcategory  field filter 
$.extend($.fn.datagrid.defaults.filters, {
authcategoryfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: authcategoryfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   authcategory   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
authcategoryeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: authcategorydatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------AEO认证类别---------//
var authtypefiltersource = [{ value: '', text: 'All'}];
var authtypedatasource = [];
authtypefiltersource.push({ value: '一般认证',text:'一般认证'  });
authtypedatasource.push({ value: '一般认证',text:'一般认证'  });
authtypefiltersource.push({ value: '高级认证',text:'高级认证'  });
authtypedatasource.push({ value: '高级认证',text:'高级认证'  });
//for datagrid authtype field  formatter
function authtypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = authtypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = authtypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   authtype  field filter 
$.extend($.fn.datagrid.defaults.filters, {
authtypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: authtypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   authtype   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
authtypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: authtypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------客户等级---------//
var clevelfiltersource = [{ value: '', text: 'All'}];
var cleveldatasource = [];
clevelfiltersource.push({ value: 'A',text:'A'  });
cleveldatasource.push({ value: 'A',text:'A'  });
clevelfiltersource.push({ value: 'B',text:'B'  });
cleveldatasource.push({ value: 'B',text:'B'  });
clevelfiltersource.push({ value: 'C',text:'C'  });
cleveldatasource.push({ value: 'C',text:'C'  });
clevelfiltersource.push({ value: 'D',text:'D'  });
cleveldatasource.push({ value: 'D',text:'D'  });
//for datagrid clevel field  formatter
function clevelformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = cleveldatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = cleveldatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   clevel  field filter 
$.extend($.fn.datagrid.defaults.filters, {
clevelfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: clevelfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   clevel   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
cleveleditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: cleveldatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------客户类型---------//
var ctypefiltersource = [{ value: '', text: 'All'}];
var ctypedatasource = [];
ctypefiltersource.push({ value: 'VIP客户',text:'VIP客户'  });
ctypedatasource.push({ value: 'VIP客户',text:'VIP客户'  });
ctypefiltersource.push({ value: '商超客户',text:'商超客户'  });
ctypedatasource.push({ value: '商超客户',text:'商超客户'  });
ctypefiltersource.push({ value: '意向客户',text:'意向客户'  });
ctypedatasource.push({ value: '意向客户',text:'意向客户'  });
ctypefiltersource.push({ value: '成交客户',text:'成交客户'  });
ctypedatasource.push({ value: '成交客户',text:'成交客户'  });
ctypefiltersource.push({ value: '普通客户',text:'普通客户'  });
ctypedatasource.push({ value: '普通客户',text:'普通客户'  });
ctypefiltersource.push({ value: '服务供应商',text:'服务供应商'  });
ctypedatasource.push({ value: '服务供应商',text:'服务供应商'  });
ctypefiltersource.push({ value: '正式供应商',text:'正式供应商'  });
ctypedatasource.push({ value: '正式供应商',text:'正式供应商'  });
ctypefiltersource.push({ value: '潜在供应商',text:'潜在供应商'  });
ctypedatasource.push({ value: '潜在供应商',text:'潜在供应商'  });
ctypefiltersource.push({ value: '货运',text:'货运'  });
ctypedatasource.push({ value: '货运',text:'货运'  });
//for datagrid ctype field  formatter
function ctypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = ctypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = ctypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   ctype  field filter 
$.extend($.fn.datagrid.defaults.filters, {
ctypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: ctypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   ctype   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
ctypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: ctypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------体积单位---------//
var cunitfiltersource = [{ value: '', text: 'All'}];
var cunitdatasource = [];
cunitfiltersource.push({ value: '升',text:'升'  });
cunitdatasource.push({ value: '升',text:'升'  });
cunitfiltersource.push({ value: '毫升',text:'毫升'  });
cunitdatasource.push({ value: '毫升',text:'毫升'  });
cunitfiltersource.push({ value: '立方米',text:'立方米'  });
cunitdatasource.push({ value: '立方米',text:'立方米'  });
//for datagrid cunit field  formatter
function cunitformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = cunitdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = cunitdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   cunit  field filter 
$.extend($.fn.datagrid.defaults.filters, {
cunitfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: cunitfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   cunit   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
cuniteditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: cunitdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------币种---------//
var curfiltersource = [{ value: '', text: 'All'}];
var curdatasource = [];
curfiltersource.push({ value: 'AUD',text:'AUD'  });
curdatasource.push({ value: 'AUD',text:'AUD'  });
curfiltersource.push({ value: 'CNY',text:'CNY'  });
curdatasource.push({ value: 'CNY',text:'CNY'  });
curfiltersource.push({ value: 'EUR',text:'EUR'  });
curdatasource.push({ value: 'EUR',text:'EUR'  });
curfiltersource.push({ value: 'GBP',text:'GBP'  });
curdatasource.push({ value: 'GBP',text:'GBP'  });
curfiltersource.push({ value: 'HKD',text:'HKD'  });
curdatasource.push({ value: 'HKD',text:'HKD'  });
curfiltersource.push({ value: 'JPY',text:'JPY'  });
curdatasource.push({ value: 'JPY',text:'JPY'  });
curfiltersource.push({ value: 'MOP',text:'MOP'  });
curdatasource.push({ value: 'MOP',text:'MOP'  });
curfiltersource.push({ value: 'USD',text:'USD'  });
curdatasource.push({ value: 'USD',text:'USD'  });
//for datagrid cur field  formatter
function curformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = curdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = curdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   cur  field filter 
$.extend($.fn.datagrid.defaults.filters, {
curfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: curfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   cur   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
cureditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: curdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------文件类型---------//
var filetypefiltersource = [{ value: '', text: 'All'}];
var filetypedatasource = [];
filetypefiltersource.push({ value: '0',text:'txt'  });
filetypedatasource.push({ value: '0',text:'txt'  });
filetypefiltersource.push({ value: '1',text:'xls'  });
filetypedatasource.push({ value: '1',text:'xls'  });
filetypefiltersource.push({ value: '10',text:'docx'  });
filetypedatasource.push({ value: '10',text:'docx'  });
filetypefiltersource.push({ value: '11',text:'py'  });
filetypedatasource.push({ value: '11',text:'py'  });
filetypefiltersource.push({ value: '12',text:'c'  });
filetypedatasource.push({ value: '12',text:'c'  });
filetypefiltersource.push({ value: '13',text:'java'  });
filetypedatasource.push({ value: '13',text:'java'  });
filetypefiltersource.push({ value: '2',text:'pdf'  });
filetypedatasource.push({ value: '2',text:'pdf'  });
filetypefiltersource.push({ value: '3',text:'xlsx'  });
filetypedatasource.push({ value: '3',text:'xlsx'  });
filetypefiltersource.push({ value: '4',text:'json'  });
filetypedatasource.push({ value: '4',text:'json'  });
filetypefiltersource.push({ value: '5',text:'js'  });
filetypedatasource.push({ value: '5',text:'js'  });
filetypefiltersource.push({ value: '6',text:'html'  });
filetypedatasource.push({ value: '6',text:'html'  });
filetypefiltersource.push({ value: '7',text:'xml'  });
filetypedatasource.push({ value: '7',text:'xml'  });
filetypefiltersource.push({ value: '8',text:'cs'  });
filetypedatasource.push({ value: '8',text:'cs'  });
filetypefiltersource.push({ value: '9',text:'doc'  });
filetypedatasource.push({ value: '9',text:'doc'  });
//for datagrid FileType field  formatter
function filetypeformatter(value, row, index) { 
     let multiple = true; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = filetypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = filetypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   FileType  field filter 
$.extend($.fn.datagrid.defaults.filters, {
filetypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: filetypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   FileType   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
filetypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: filetypedatasource,
         multiple: true,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------跟进方式---------//
var followfiltersource = [{ value: '', text: 'All'}];
var followdatasource = [];
followfiltersource.push({ value: 'Line',text:'Line'  });
followdatasource.push({ value: 'Line',text:'Line'  });
followfiltersource.push({ value: 'QQ',text:'QQ'  });
followdatasource.push({ value: 'QQ',text:'QQ'  });
followfiltersource.push({ value: 'Skype',text:'Skype'  });
followdatasource.push({ value: 'Skype',text:'Skype'  });
followfiltersource.push({ value: 'WhatsApp',text:'WhatsApp'  });
followdatasource.push({ value: 'WhatsApp',text:'WhatsApp'  });
followfiltersource.push({ value: '展会洽谈',text:'展会洽谈'  });
followdatasource.push({ value: '展会洽谈',text:'展会洽谈'  });
followfiltersource.push({ value: '微信',text:'微信'  });
followdatasource.push({ value: '微信',text:'微信'  });
followfiltersource.push({ value: '拜访',text:'拜访'  });
followdatasource.push({ value: '拜访',text:'拜访'  });
followfiltersource.push({ value: '电话',text:'电话'  });
followdatasource.push({ value: '电话',text:'电话'  });
followfiltersource.push({ value: '短信',text:'短信'  });
followdatasource.push({ value: '短信',text:'短信'  });
followfiltersource.push({ value: '邮件',text:'邮件'  });
followdatasource.push({ value: '邮件',text:'邮件'  });
//for datagrid follow field  formatter
function followformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = followdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = followdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   follow  field filter 
$.extend($.fn.datagrid.defaults.filters, {
followfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: followfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   follow   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
followeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: followdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------跟进状态---------//
var followstatusfiltersource = [{ value: '', text: 'All'}];
var followstatusdatasource = [];
followstatusfiltersource.push({ value: '下单意向',text:'下单意向'  });
followstatusdatasource.push({ value: '下单意向',text:'下单意向'  });
followstatusfiltersource.push({ value: '初次接触',text:'初次接触'  });
followstatusdatasource.push({ value: '初次接触',text:'初次接触'  });
followstatusfiltersource.push({ value: '售后服务',text:'售后服务'  });
followstatusdatasource.push({ value: '售后服务',text:'售后服务'  });
followstatusfiltersource.push({ value: '客户投诉',text:'客户投诉'  });
followstatusdatasource.push({ value: '客户投诉',text:'客户投诉'  });
followstatusfiltersource.push({ value: '正式报价',text:'正式报价'  });
followstatusdatasource.push({ value: '正式报价',text:'正式报价'  });
followstatusfiltersource.push({ value: '签约成交',text:'签约成交'  });
followstatusdatasource.push({ value: '签约成交',text:'签约成交'  });
followstatusfiltersource.push({ value: '邮件沟通',text:'邮件沟通'  });
followstatusdatasource.push({ value: '邮件沟通',text:'邮件沟通'  });
followstatusfiltersource.push({ value: '首次报价',text:'首次报价'  });
followstatusdatasource.push({ value: '首次报价',text:'首次报价'  });
//for datagrid followstatus field  formatter
function followstatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = followstatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = followstatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   followstatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
followstatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: followstatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   followstatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
followstatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: followstatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------跟进方式---------//
var followtypefiltersource = [{ value: '', text: 'All'}];
var followtypedatasource = [];
followtypefiltersource.push({ value: 'QQ',text:'QQ'  });
followtypedatasource.push({ value: 'QQ',text:'QQ'  });
followtypefiltersource.push({ value: 'Skype',text:'Skype'  });
followtypedatasource.push({ value: 'Skype',text:'Skype'  });
followtypefiltersource.push({ value: 'WhatsApp',text:'WhatsApp'  });
followtypedatasource.push({ value: 'WhatsApp',text:'WhatsApp'  });
followtypefiltersource.push({ value: '微信',text:'微信'  });
followtypedatasource.push({ value: '微信',text:'微信'  });
followtypefiltersource.push({ value: '拜访',text:'拜访'  });
followtypedatasource.push({ value: '拜访',text:'拜访'  });
followtypefiltersource.push({ value: '电话',text:'电话'  });
followtypedatasource.push({ value: '电话',text:'电话'  });
followtypefiltersource.push({ value: '短信',text:'短信'  });
followtypedatasource.push({ value: '短信',text:'短信'  });
followtypefiltersource.push({ value: '邮件',text:'邮件'  });
followtypedatasource.push({ value: '邮件',text:'邮件'  });
//for datagrid followtype field  formatter
function followtypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = followtypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = followtypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   followtype  field filter 
$.extend($.fn.datagrid.defaults.filters, {
followtypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: followtypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   followtype   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
followtypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: followtypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------行业---------//
var industryfiltersource = [{ value: '', text: 'All'}];
var industrydatasource = [];
industryfiltersource.push({ value: '制造行业',text:'制造行业'  });
industrydatasource.push({ value: '制造行业',text:'制造行业'  });
industryfiltersource.push({ value: '外贸行业',text:'外贸行业'  });
industrydatasource.push({ value: '外贸行业',text:'外贸行业'  });
industryfiltersource.push({ value: '服务行业',text:'服务行业'  });
industrydatasource.push({ value: '服务行业',text:'服务行业'  });
//for datagrid industry field  formatter
function industryformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = industrydatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = industrydatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   industry  field filter 
$.extend($.fn.datagrid.defaults.filters, {
industryfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: industryfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   industry   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
industryeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: industrydatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------询价任务状态---------//
var intstatusfiltersource = [{ value: '', text: 'All'}];
var intstatusdatasource = [];
intstatusfiltersource.push({ value: '生效',text:'生效'  });
intstatusdatasource.push({ value: '生效',text:'生效'  });
intstatusfiltersource.push({ value: '草拟',text:'草拟'  });
intstatusdatasource.push({ value: '草拟',text:'草拟'  });
//for datagrid intstatus field  formatter
function intstatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = intstatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = intstatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   intstatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
intstatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: intstatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   intstatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
intstatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: intstatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------禁用标志---------//
var isdisabledfiltersource = [{ value: '', text: 'All'}];
var isdisableddatasource = [];
isdisabledfiltersource.push({ value: '0',text:'未禁用'  });
isdisableddatasource.push({ value: '0',text:'未禁用'  });
isdisabledfiltersource.push({ value: '1',text:'已禁用'  });
isdisableddatasource.push({ value: '1',text:'已禁用'  });
//for datagrid IsDisabled field  formatter
function isdisabledformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = isdisableddatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = isdisableddatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   IsDisabled  field filter 
$.extend($.fn.datagrid.defaults.filters, {
isdisabledfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: isdisabledfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   IsDisabled   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
isdisablededitor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: isdisableddatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------启用标志---------//
var isenabledfiltersource = [{ value: '', text: 'All'}];
var isenableddatasource = [];
isenabledfiltersource.push({ value: '0',text:'未启用'  });
isenableddatasource.push({ value: '0',text:'未启用'  });
isenabledfiltersource.push({ value: '1',text:'已启用'  });
isenableddatasource.push({ value: '1',text:'已启用'  });
//for datagrid IsEnabled field  formatter
function isenabledformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = isenableddatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = isenableddatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   IsEnabled  field filter 
$.extend($.fn.datagrid.defaults.filters, {
isenabledfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: isenabledfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   IsEnabled   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
isenablededitor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: isenableddatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------已读标志---------//
var isnewfiltersource = [{ value: '', text: 'All'}];
var isnewdatasource = [];
isnewfiltersource.push({ value: '0',text:'未读'  });
isnewdatasource.push({ value: '0',text:'未读'  });
isnewfiltersource.push({ value: '1',text:'已读'  });
isnewdatasource.push({ value: '1',text:'已读'  });
//for datagrid IsNew field  formatter
function isnewformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = isnewdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = isnewdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   IsNew  field filter 
$.extend($.fn.datagrid.defaults.filters, {
isnewfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: isnewfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   IsNew   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
isneweditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: isnewdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------通知标志---------//
var isnoticefiltersource = [{ value: '', text: 'All'}];
var isnoticedatasource = [];
isnoticefiltersource.push({ value: '0',text:'未发'  });
isnoticedatasource.push({ value: '0',text:'未发'  });
isnoticefiltersource.push({ value: '1',text:'已发'  });
isnoticedatasource.push({ value: '1',text:'已发'  });
//for datagrid IsNotice field  formatter
function isnoticeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = isnoticedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = isnoticedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   IsNotice  field filter 
$.extend($.fn.datagrid.defaults.filters, {
isnoticefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: isnoticefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   IsNotice   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
isnoticeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: isnoticedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------NLogLevel---------//
var levelfiltersource = [{ value: '', text: 'All'}];
var leveldatasource = [];
levelfiltersource.push({ value: 'Debug',text:'Debug'  });
leveldatasource.push({ value: 'Debug',text:'Debug'  });
levelfiltersource.push({ value: 'Error',text:'Error'  });
leveldatasource.push({ value: 'Error',text:'Error'  });
levelfiltersource.push({ value: 'Fatal',text:'Fatal'  });
leveldatasource.push({ value: 'Fatal',text:'Fatal'  });
levelfiltersource.push({ value: 'Info',text:'Info'  });
leveldatasource.push({ value: 'Info',text:'Info'  });
levelfiltersource.push({ value: 'Trace',text:'Trace'  });
leveldatasource.push({ value: 'Trace',text:'Trace'  });
levelfiltersource.push({ value: 'Warn',text:'Warn'  });
leveldatasource.push({ value: 'Warn',text:'Warn'  });
//for datagrid Level field  formatter
function levelformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = leveldatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = leveldatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   Level  field filter 
$.extend($.fn.datagrid.defaults.filters, {
levelfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: levelfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   Level   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
leveleditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: leveldatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------长度单位---------//
var lunitfiltersource = [{ value: '', text: 'All'}];
var lunitdatasource = [];
lunitfiltersource.push({ value: '千米',text:'千米'  });
lunitdatasource.push({ value: '千米',text:'千米'  });
lunitfiltersource.push({ value: '厘米',text:'厘米'  });
lunitdatasource.push({ value: '厘米',text:'厘米'  });
lunitfiltersource.push({ value: '毫米',text:'毫米'  });
lunitdatasource.push({ value: '毫米',text:'毫米'  });
lunitfiltersource.push({ value: '米',text:'米'  });
lunitdatasource.push({ value: '米',text:'米'  });
//for datagrid lunit field  formatter
function lunitformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = lunitdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = lunitdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   lunit  field filter 
$.extend($.fn.datagrid.defaults.filters, {
lunitfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: lunitfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   lunit   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
luniteditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: lunitdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------日志分组---------//
var messagegroupfiltersource = [{ value: '', text: 'All'}];
var messagegroupdatasource = [];
messagegroupfiltersource.push({ value: '0',text:'系统操作'  });
messagegroupdatasource.push({ value: '0',text:'系统操作'  });
messagegroupfiltersource.push({ value: '1',text:'业务操作'  });
messagegroupdatasource.push({ value: '1',text:'业务操作'  });
messagegroupfiltersource.push({ value: '2',text:'接口操作'  });
messagegroupdatasource.push({ value: '2',text:'接口操作'  });
//for datagrid MessageGroup field  formatter
function messagegroupformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = messagegroupdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = messagegroupdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   MessageGroup  field filter 
$.extend($.fn.datagrid.defaults.filters, {
messagegroupfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: messagegroupfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   MessageGroup   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
messagegroupeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: messagegroupdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------日志类型---------//
var messagetypefiltersource = [{ value: '', text: 'All'}];
var messagetypedatasource = [];
messagetypefiltersource.push({ value: '0',text:'Information'  });
messagetypedatasource.push({ value: '0',text:'Information'  });
messagetypefiltersource.push({ value: '1',text:'Error'  });
messagetypedatasource.push({ value: '1',text:'Error'  });
messagetypefiltersource.push({ value: '2',text:'Alert'  });
messagetypedatasource.push({ value: '2',text:'Alert'  });
//for datagrid MessageType field  formatter
function messagetypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = messagetypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = messagetypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   MessageType  field filter 
$.extend($.fn.datagrid.defaults.filters, {
messagetypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: messagetypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   MessageType   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
messagetypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: messagetypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------通知分组---------//
var notifygroupfiltersource = [{ value: '', text: 'All'}];
var notifygroupdatasource = [];
notifygroupfiltersource.push({ value: '0',text:'系统推送'  });
notifygroupdatasource.push({ value: '0',text:'系统推送'  });
notifygroupfiltersource.push({ value: '1',text:'业务推送'  });
notifygroupdatasource.push({ value: '1',text:'业务推送'  });
notifygroupfiltersource.push({ value: '2',text:'接口推送'  });
notifygroupdatasource.push({ value: '2',text:'接口推送'  });
notifygroupfiltersource.push({ value: '3',text:'手工推送'  });
notifygroupdatasource.push({ value: '3',text:'手工推送'  });
//for datagrid NotifyGroup field  formatter
function notifygroupformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = notifygroupdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = notifygroupdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   NotifyGroup  field filter 
$.extend($.fn.datagrid.defaults.filters, {
notifygroupfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: notifygroupfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   NotifyGroup   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
notifygroupeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: notifygroupdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------价格类型---------//
var pricetypefiltersource = [{ value: '', text: 'All'}];
var pricetypedatasource = [];
pricetypefiltersource.push({ value: '不开票不含国内运费',text:'不开票不含国内运费'  });
pricetypedatasource.push({ value: '不开票不含国内运费',text:'不开票不含国内运费'  });
pricetypefiltersource.push({ value: '不开票含国内运费',text:'不开票含国内运费'  });
pricetypedatasource.push({ value: '不开票含国内运费',text:'不开票含国内运费'  });
pricetypefiltersource.push({ value: '开票不含国内运费',text:'开票不含国内运费'  });
pricetypedatasource.push({ value: '开票不含国内运费',text:'开票不含国内运费'  });
pricetypefiltersource.push({ value: '开票含国内运费',text:'开票含国内运费'  });
pricetypedatasource.push({ value: '开票含国内运费',text:'开票含国内运费'  });
//for datagrid pricetype field  formatter
function pricetypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = pricetypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = pricetypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   pricetype  field filter 
$.extend($.fn.datagrid.defaults.filters, {
pricetypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: pricetypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   pricetype   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
pricetypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: pricetypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------优先级---------//
var priorityfiltersource = [{ value: '', text: 'All'}];
var prioritydatasource = [];
priorityfiltersource.push({ value: '1',text:'高'  });
prioritydatasource.push({ value: '1',text:'高'  });
priorityfiltersource.push({ value: '2',text:'中'  });
prioritydatasource.push({ value: '2',text:'中'  });
priorityfiltersource.push({ value: '3',text:'低'  });
prioritydatasource.push({ value: '3',text:'低'  });
priorityfiltersource.push({ value: '4',text:'低级'  });
prioritydatasource.push({ value: '4',text:'低级'  });
//for datagrid Priority field  formatter
function priorityformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = prioritydatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = prioritydatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   Priority  field filter 
$.extend($.fn.datagrid.defaults.filters, {
priorityfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: priorityfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   Priority   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
priorityeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: prioritydatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------包装单位---------//
var punitfiltersource = [{ value: '', text: 'All'}];
var punitdatasource = [];
punitfiltersource.push({ value: '打',text:'打'  });
punitdatasource.push({ value: '打',text:'打'  });
punitfiltersource.push({ value: '板',text:'板'  });
punitdatasource.push({ value: '板',text:'板'  });
punitfiltersource.push({ value: '桶',text:'桶'  });
punitdatasource.push({ value: '桶',text:'桶'  });
punitfiltersource.push({ value: '箱',text:'箱'  });
punitdatasource.push({ value: '箱',text:'箱'  });
//for datagrid punit field  formatter
function punitformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = punitdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = punitdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   punit  field filter 
$.extend($.fn.datagrid.defaults.filters, {
punitfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: punitfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   punit   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
puniteditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: punitdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------NLogResolved---------//
var resolvedfiltersource = [{ value: '', text: 'All'}];
var resolveddatasource = [];
resolvedfiltersource.push({ value: 'false',text:'未处理'  });
resolveddatasource.push({ value: 'false',text:'未处理'  });
resolvedfiltersource.push({ value: 'true',text:'已处理'  });
resolveddatasource.push({ value: 'true',text:'已处理'  });
//for datagrid Resolved field  formatter
function resolvedformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = resolveddatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = resolveddatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   Resolved  field filter 
$.extend($.fn.datagrid.defaults.filters, {
resolvedfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: resolvedfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   Resolved   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
resolvededitor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: resolveddatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------询价单状态---------//
var rfqstatusfiltersource = [{ value: '', text: 'All'}];
var rfqstatusdatasource = [];
rfqstatusfiltersource.push({ value: '待审',text:'待审'  });
rfqstatusdatasource.push({ value: '待审',text:'待审'  });
rfqstatusfiltersource.push({ value: '生效',text:'生效'  });
rfqstatusdatasource.push({ value: '生效',text:'生效'  });
rfqstatusfiltersource.push({ value: '草拟',text:'草拟'  });
rfqstatusdatasource.push({ value: '草拟',text:'草拟'  });
//for datagrid rfqstatus field  formatter
function rfqstatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = rfqstatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = rfqstatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   rfqstatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
rfqstatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: rfqstatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   rfqstatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
rfqstatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: rfqstatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------性别---------//
var sexfiltersource = [{ value: '', text: 'All'}];
var sexdatasource = [];
sexfiltersource.push({ value: '女',text:'女'  });
sexdatasource.push({ value: '女',text:'女'  });
sexfiltersource.push({ value: '男',text:'男'  });
sexdatasource.push({ value: '男',text:'男'  });
//for datagrid sex field  formatter
function sexformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = sexdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = sexdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   sex  field filter 
$.extend($.fn.datagrid.defaults.filters, {
sexfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: sexfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   sex   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
sexeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: sexdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------客户来源---------//
var sourcefiltersource = [{ value: '', text: 'All'}];
var sourcedatasource = [];
sourcefiltersource.push({ value: '互联网',text:'互联网'  });
sourcedatasource.push({ value: '互联网',text:'互联网'  });
sourcefiltersource.push({ value: '促销活动',text:'促销活动'  });
sourcedatasource.push({ value: '促销活动',text:'促销活动'  });
sourcefiltersource.push({ value: '内部员工转让',text:'内部员工转让'  });
sourcedatasource.push({ value: '内部员工转让',text:'内部员工转让'  });
sourcefiltersource.push({ value: '合作伙伴',text:'合作伙伴'  });
sourcedatasource.push({ value: '合作伙伴',text:'合作伙伴'  });
sourcefiltersource.push({ value: '国外展会',text:'国外展会'  });
sourcedatasource.push({ value: '国外展会',text:'国外展会'  });
sourcefiltersource.push({ value: '熟人介绍',text:'熟人介绍'  });
sourcedatasource.push({ value: '熟人介绍',text:'熟人介绍'  });
sourcefiltersource.push({ value: '社交网站',text:'社交网站'  });
sourcedatasource.push({ value: '社交网站',text:'社交网站'  });
sourcefiltersource.push({ value: '老客户介绍',text:'老客户介绍'  });
sourcedatasource.push({ value: '老客户介绍',text:'老客户介绍'  });
sourcefiltersource.push({ value: '阿里巴巴',text:'阿里巴巴'  });
sourcedatasource.push({ value: '阿里巴巴',text:'阿里巴巴'  });
//for datagrid source field  formatter
function sourceformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = sourcedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = sourcedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   source  field filter 
$.extend($.fn.datagrid.defaults.filters, {
sourcefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: sourcefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   source   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
sourceeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: sourcedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------商机阶段---------//
var stagefiltersource = [{ value: '', text: 'All'}];
var stagedatasource = [];
stagefiltersource.push({ value: '商务谈判',text:'商务谈判'  });
stagedatasource.push({ value: '商务谈判',text:'商务谈判'  });
stagefiltersource.push({ value: '投标/报价',text:'投标/报价'  });
stagedatasource.push({ value: '投标/报价',text:'投标/报价'  });
stagefiltersource.push({ value: '方案制定',text:'方案制定'  });
stagedatasource.push({ value: '方案制定',text:'方案制定'  });
stagefiltersource.push({ value: '样品开模',text:'样品开模'  });
stagedatasource.push({ value: '样品开模',text:'样品开模'  });
stagefiltersource.push({ value: '演示方案',text:'演示方案'  });
stagedatasource.push({ value: '演示方案',text:'演示方案'  });
stagefiltersource.push({ value: '竞争',text:'竞争'  });
stagedatasource.push({ value: '竞争',text:'竞争'  });
stagefiltersource.push({ value: '签约',text:'签约'  });
stagedatasource.push({ value: '签约',text:'签约'  });
//for datagrid stage field  formatter
function stageformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = stagedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = stagedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   stage  field filter 
$.extend($.fn.datagrid.defaults.filters, {
stagefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: stagefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   stage   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
stageeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: stagedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------Test---------//
var statusfiltersource = [{ value: '', text: 'All'}];
var statusdatasource = [];
statusfiltersource.push({ value: '0',text:'新增'  });
statusdatasource.push({ value: '0',text:'新增'  });
statusfiltersource.push({ value: '1',text:'修改'  });
statusdatasource.push({ value: '1',text:'修改'  });
statusfiltersource.push({ value: '2',text:'异常'  });
statusdatasource.push({ value: '2',text:'异常'  });
statusfiltersource.push({ value: '3',text:'关闭'  });
statusdatasource.push({ value: '3',text:'关闭'  });
//for datagrid Status field  formatter
function statusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = statusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = statusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   Status  field filter 
$.extend($.fn.datagrid.defaults.filters, {
statusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: statusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   Status   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
statuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: statusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------单位代码---------//
var unitfiltersource = [{ value: '', text: 'All'}];
var unitdatasource = [];
unitfiltersource.push({ value: '个',text:'个'  });
unitdatasource.push({ value: '个',text:'个'  });
unitfiltersource.push({ value: '千克',text:'千克'  });
unitdatasource.push({ value: '千克',text:'千克'  });
unitfiltersource.push({ value: '台',text:'台'  });
unitdatasource.push({ value: '台',text:'台'  });
unitfiltersource.push({ value: '套',text:'套'  });
unitdatasource.push({ value: '套',text:'套'  });
unitfiltersource.push({ value: '座',text:'座'  });
unitdatasource.push({ value: '座',text:'座'  });
unitfiltersource.push({ value: '架',text:'架'  });
unitdatasource.push({ value: '架',text:'架'  });
unitfiltersource.push({ value: '艘',text:'艘'  });
unitdatasource.push({ value: '艘',text:'艘'  });
unitfiltersource.push({ value: '辆',text:'辆'  });
unitdatasource.push({ value: '辆',text:'辆'  });
//for datagrid unit field  formatter
function unitformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = unitdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = unitdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   unit  field filter 
$.extend($.fn.datagrid.defaults.filters, {
unitfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: unitfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   unit   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
uniteditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: unitdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------紧急程度---------//
var urgencyfiltersource = [{ value: '', text: 'All'}];
var urgencydatasource = [];
urgencyfiltersource.push({ value: '正常',text:'正常'  });
urgencydatasource.push({ value: '正常',text:'正常'  });
urgencyfiltersource.push({ value: '紧急',text:'紧急'  });
urgencydatasource.push({ value: '紧急',text:'紧急'  });
urgencyfiltersource.push({ value: '非常紧急',text:'非常紧急'  });
urgencydatasource.push({ value: '非常紧急',text:'非常紧急'  });
//for datagrid urgency field  formatter
function urgencyformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = urgencydatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = urgencydatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   urgency  field filter 
$.extend($.fn.datagrid.defaults.filters, {
urgencyfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: urgencyfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   urgency   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
urgencyeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: urgencydatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------仓库类型---------//
var whtypefiltersource = [{ value: '', text: 'All'}];
var whtypedatasource = [];
whtypefiltersource.push({ value: '保税仓',text:'保税仓'  });
whtypedatasource.push({ value: '保税仓',text:'保税仓'  });
whtypefiltersource.push({ value: '普通仓',text:'普通仓'  });
whtypedatasource.push({ value: '普通仓',text:'普通仓'  });
//for datagrid whtype field  formatter
function whtypeformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = whtypedatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = whtypedatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   whtype  field filter 
$.extend($.fn.datagrid.defaults.filters, {
whtypefilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: whtypefiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   whtype   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
whtypeeditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: whtypedatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------任务状态---------//
var workstatusfiltersource = [{ value: '', text: 'All'}];
var workstatusdatasource = [];
workstatusfiltersource.push({ value: '0',text:'等待指派'  });
workstatusdatasource.push({ value: '0',text:'等待指派'  });
workstatusfiltersource.push({ value: '1',text:'未开始'  });
workstatusdatasource.push({ value: '1',text:'未开始'  });
workstatusfiltersource.push({ value: '2',text:'进行中'  });
workstatusdatasource.push({ value: '2',text:'进行中'  });
workstatusfiltersource.push({ value: '3',text:'已完成'  });
workstatusdatasource.push({ value: '3',text:'已完成'  });
workstatusfiltersource.push({ value: '4',text:'关闭'  });
workstatusdatasource.push({ value: '4',text:'关闭'  });
//for datagrid workstatus field  formatter
function workstatusformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = workstatusdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = workstatusdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   workstatus  field filter 
$.extend($.fn.datagrid.defaults.filters, {
workstatusfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: workstatusfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   workstatus   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
workstatuseditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: workstatusdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
//-------重量单位---------//
var wunitfiltersource = [{ value: '', text: 'All'}];
var wunitdatasource = [];
wunitfiltersource.push({ value: '克',text:'克'  });
wunitdatasource.push({ value: '克',text:'克'  });
wunitfiltersource.push({ value: '千克',text:'千克'  });
wunitdatasource.push({ value: '千克',text:'千克'  });
wunitfiltersource.push({ value: '吨',text:'吨'  });
wunitdatasource.push({ value: '吨',text:'吨'  });
//for datagrid wunit field  formatter
function wunitformatter(value, row, index) { 
     let multiple = false; 
     if (value === null || value === '' || value === undefined) 
     { 
         return "";
     } 
     if (multiple) { 
         let valarray = value.split(','); 
         let result = wunitdatasource.filter(item => valarray.includes(item.value));
         let textarray = result.map(x => x.text);
         if (textarray.length > 0)
             return textarray.join(",");
         else 
             return value;
      } else { 
         let result = wunitdatasource.filter(x => x.value == value);
               if (result.length > 0)
                    return result[0].text;
               else
                    return value;
       } 
 } 
//for datagrid   wunit  field filter 
$.extend($.fn.datagrid.defaults.filters, {
wunitfilter: {
     init: function(container, options) {
        var input = $('<select class="easyui-combobox" >').appendTo(container);
        var myoptions = {
             panelHeight: 'auto',
             editable: false,
             data: wunitfiltersource ,
             onChange: function () {
                input.trigger('combobox.filter');
             }
         };
         $.extend(options, myoptions);
         input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
         return input;
      },
     destroy: function(target) {
                  
     },
     getValue: function(target) {
         return $(target).combobox('getValue');
     },
     setValue: function(target, value) {
         $(target).combobox('setValue', value);
     },
     resize: function(target, width) {
         $(target).combobox('resize', width);
     }
   }
});
//for datagrid   wunit   field  editor 
$.extend($.fn.datagrid.defaults.editors, {
wuniteditor: {
     init: function(container, options) {
        var input = $('<input type="text">').appendTo(container);
        var myoptions = {
         panelHeight: 'auto',
         editable: false,
         data: wunitdatasource,
         multiple: false,
         valueField: 'value',
         textField: 'text'
     };
    $.extend(options, myoptions);
           input.combobox(options);
         input.combobox('textbox').bind('keydown', function (e) {   
            if (e.keyCode === 13) {
              $(e.target).emulateTab();
            }
          });  
           return input;
       },
     destroy: function(target) {
         $(target).combobox('destroy');
        },
     getValue: function(target) {
        let opts = $(target).combobox('options');
        if (opts.multiple) {
           return $(target).combobox('getValues').join(opts.separator);
         } else {
            return $(target).combobox('getValue');
         }
        },
     setValue: function(target, value) {
         let opts = $(target).combobox('options');
         if (opts.multiple) {
             if (value == '' || value == null) { 
                 $(target).combobox('clear'); 
              } else { 
                  $(target).combobox('setValues', value.split(opts.separator));
               }
          }
          else {
             $(target).combobox('setValue', value);
           }
         },
     resize: function(target, width) {
         $(target).combobox('resize', width);
        }
  }  
});
