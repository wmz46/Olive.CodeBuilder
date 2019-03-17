# Olive.CodeBuilder
vs插件项目，一个基于T4的模板生成器

## 一、主要功能

  1.根据数据库的表和T4模板自动生成代码并添加到项目中。<br>
## 二、存在问题：

  1.目前只支持sqlserver 数据库。<br>
  2.T4模板并未集成到软件中，需要另外添加。<br>

## 三、软件界面：

![界面图片](https://github.com/wmz46/Olive.CodeBuilder/blob/master/doc/images/readme_1.png)

## 四、参数说明：
```  Java
<#@ template hostspecific="true" debug="true" #>
<#@output extension=".cs"#>  
<# 
  TableHost host = (TableHost)(Host);
  string tableName = host.TableName;//表名
  string className = host.TableName;
  var dtColumns = host.Columns;
#>  
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace <#=host.NameSpace#>
{
    /// <summary>
    /// 实体类<#=className#>。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Table("<#=tableName#>")]
    [Serializable]
    public partial class <#=className#> : BaseEntity,ICloneable
    {
<#foreach(DataRow item in dtColumns.Rows){
	var columnName = item["ColumnName"].ToString();
	var cisNull = (item["cisNull"].ToString()=="√");
	var typeName = item["TypeName"].ToString();
	if(typeName == "varchar"){
		typeName = "string";
	}else if(typeName == "datetime"){
		typeName = "DateTime";
	}
	if(cisNull && typeName!="string"){
		typeName += "?";
	}
#>
<#if(!cisNull){#>
		[Required]
<#}#>
		public <#=typeName#> <#=columnName#> {get;set;}
<#}#>
	}
}
``` 
