# Olive.CodeBuilder
vs插件项目，一个基于T4的模板生成器

## 一、主要功能

  1.根据数据库的表和T4模板自动生成代码并添加到项目中。<br>


## 二、软件界面及使用说明：
  1.在VS解决项目浏览器中选择要添加代码的文件夹，右键弹出菜单，选择代码生成。<br>
![界面图片](https://github.com/wmz46/Olive.CodeBuilder/blob/master/doc/images/readme_2.png)<br>
  2.弹出以下窗口，输入数据库连接字符串，点击“载入数据库”按钮。点击“选择文件夹”，选择T4模板所在文件夹，并点击“载入T4模板”，最后勾选要生成的表和模板，点击“生成代码”即可。表和视图右侧的输入框在输入后，会实时根据字符串模糊匹配过滤掉多余表及视图（不区分大小写）。<br>
![界面图片](https://github.com/wmz46/Olive.CodeBuilder/blob/master/doc/images/readme_1.png)

## 三、参数说明：
  T4模板需要注意一下几点：<br>
  1.hostspecific="true" 必须使用Host传递参数。<br>
  2.如需要保存成其他文件后缀，请修改\<#@output extension=".cs"#>  <br>
  3.Host参数如下，--表示从属Columns下的字段名<br>


| 参数名    | 类型       | 说明               |
| ------------- |:-------------:| :-----:|
| TableName | string    | 完整表名           |
| NameSpace | string    |选中文件夹的命名空间 |
| Columns   | DataTable | 列信息             |
|   --ColumnName| DataTable | 列名             |
|    --TypeName   | DataTable | 字段类型，对应c#中的类型            |
|    --cisNull   | DataTable | √表示可为空             |
|    --CharLength   | DataTable | 字段长度             |
|    --isPK   | DataTable | √表示主键            |
|    --IsIdentity   | string | √表示自增             |
|    --DeText   | string | 字段中文备注             |

示例模板如下：
```  C#
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
	var dbText = item["DeText"].ToString();	
	if(cisNull && typeName!="string"){
		typeName += "?";
	}
#>
                /// <summary>
		/// <#=dbText#>
		/// </summary>
<#if(!cisNull){#>
		[Required]
<#}#>
		public <#=typeName#> <#=columnName#> {get;set;}
<#}#>
	}
}
``` 
## 四、存在问题：

  1.目前只支持sqlserver 数据库。<br>
  2.T4模板并未集成到软件中，需要另外添加，需要有一定T4模板基础。<br>
  3.除了类可以通过解析生成的类名生成对应文件名的文件，其他文件格式只能生成以表名命名的文件。<br>
