using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aim.Portal.Data
{
    public class ExportTemplateColumnNodeConfigProcessor : TemplateConfigProcessor<ExportTemplateColumnNode>
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public override string Prefix
        {
            get { return "@"; }
        }

        /// <summary>
        /// 预处理
        /// </summary>
        /// <param name="configstr"></param>
        /// <returns></returns>
        protected override string Preprocess(string configstr)
        {
            string[] colTagFields = configstr.Split(',');

            for (int i = 0; i < colTagFields.Length; i++)
            {
                if (i == 0)
                {
                    if (!colTagFields[i].Contains(":"))
                    {
                        colTagFields[i] = String.Format("ColumnName:\"{0}\"", colTagFields[i]);
                    }
                }

                if (colTagFields[i].Trim() == "IsSingle")
                {
                    colTagFields[i] = "IsSingle:true";
                }
            }

            return StringHelper.Join(colTagFields);
        }

        /// <summary>
        /// 后续处理
        /// </summary>
        /// <param name="configobj"></param>
        /// <returns></returns>
        protected override ExportTemplateColumnNode Postprocess(ExportTemplateColumnNode configobj)
        {
            if (String.IsNullOrEmpty(configobj.Name))
            {
                configobj.Name = configobj.ColumnName;
            }

            return configobj;
        }
    }

    public class ExportTemplateCommandNodeConfigProcessor : TemplateConfigProcessor<ExportTemplateCommandNode>
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public override string Prefix
        {
            get { return "$"; }
        }

        /// <summary>
        /// 预处理
        /// </summary>
        /// <param name="configstr"></param>
        /// <returns></returns>
        protected override string Preprocess(string configstr)
        {
            string[] colTagFields = configstr.Split(',');

            for (int i = 0; i < colTagFields.Length; i++)
            {
                if (i == 0)
                {
                    if (!colTagFields[i].Contains(":"))
                    {
                        colTagFields[i] = String.Format("CommandCode:\"{0}\"", colTagFields[i]);
                    }
                }
                
                if (colTagFields[i].Trim() == "IsTransaction")
                {
                    colTagFields[i] = "IsTransaction:true";
                }
            }

            return StringHelper.Join(colTagFields);
        }

        /// <summary>
        /// 后续处理
        /// </summary>
        /// <param name="configobj"></param>
        /// <returns></returns>
        protected override ExportTemplateCommandNode Postprocess(ExportTemplateCommandNode configobj)
        {
            return configobj;
        }
    }

    public class ExportTemplatePropertyNodeConfigProcessor : TemplateConfigProcessor<ExportTemplatePropertyNode>
    {
        /// <summary>
        /// 前缀
        /// </summary>
        public override string Prefix
        {
            get { return "#"; }
        }
    }
}
