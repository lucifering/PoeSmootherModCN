# PoeSmootherModCN #
===================================  
中文版本PoeSmoother，带MOD特效替换功能  

English README:  

https://github.com/lucifering/PoeSmootherModCN/blob/master/README_EN.md  


下载地址：

https://github.com/lucifering/PoeSmootherModCN/archive/master.zip

（开发IDE：VS2015。bin\Release 文件夹下面的就是生成的结果）

或：  
http://pan.baidu.com/s/1mi7bSqG （百度盘里的可能不是最新的）
  
  

项目基于PoeSmoother：https://github.com/thisTwo/PoeSmoother
__________________________________________________________

**程序需要 Microsoft .NET Framework 4.5 运行环境。**  

**修改前请备份 GGPK 文件**  

**可能会被Ban！！！**  


***1.备份你的 Content.ggpk 文件。***  

***2.打开PoeSmoother.exe，选择你的 Content.ggpk 文件。***  

***3.等待 100%：***  

<code>
开始解析 GGPK...  

10.00%  

20.00%  

30.01%  

40.02%  

50.02%  

60.02%  

70.02%  

80.17%  

90.17%  

100.00%  


正在建立目录树...  

遍历目录树....  

全部完成!  

</code>

***4.勾选你想要的功能，自动打入补丁，如果想取消，那么只要再点一次取消勾选即可。***  


***5.进入游戏查看效果。***  




简化功能（防卡机）：

![image](https://github.com/lucifering/PoeSmootherModCN/blob/master/Screenshot/2.jpg)


自定义MOD：

![image](https://github.com/lucifering/PoeSmootherModCN/blob/master/Screenshot/1.jpg)



![image](https://github.com/lucifering/PoeSmootherModCN/blob/master/Screenshot/3.jpg)


点击下方的国旗图标可以进行切换语言（目前只有简体中文和英文）。

另外这个程序也可以当做打补丁和汉化的工具，把补丁或汉化补丁拖到左边侧边栏即可，提示替换，点击确定即可替换文件。
（也支持amsco的汉化包）   


我的博客:**http://poetw.blog.163.com/** 




  
  
# 不修改程序的情况下进行自定义菜单 #
===================================    

   
 2017年4月5日17 由于mod的变动可能比较大，所以把这个功能作为可配置的，用户可以自己添加mod tab选项自定义功能。

MOD.js文件是一个json格式的文件（文件编码是UTF-8，如果出现乱码那么就是保存文件的编码问题），里面配置了mod tab需要显示的项目，和用于替换的用的文件路径。   
   
   修改文件前请确定自己熟悉JSON格式。   
   
基本的格式如下：  
<code>
[
    {
        "Name": "- 黄色字的内容",
        "Items": [
            {               
                "Content": "- 子项目1",              
                "ToolTip": "子项目1的鼠标提示内容",
                "Tag": {
                    "ToolTipImg": "/mod/文件夹下的预览图",
                    "NewMOD": ["这里是打勾后要替换的新特效路径"],
                    "OldMOD": ["这里是取消打勾后要还原的新特效路径"]
                }
            }
        ]
    } 
]
</code>
  
  1.注意最外层是一个数组。   
  
  2.注意items是一个数组。   
  
  3.注意NewMOD和OldMOD也是数组，并且长度相同。   
  
  4.注意NewMOD和OldMOD的路径是config开头到对应的修改补丁的Art或者Metadata，例子： "NewMOD": ["config/MOD/CharacterEssenceWings/newEffects/Metadata"]   
  
  
  
  
   效果如：  
   
   
![image](https://github.com/lucifering/PoeSmootherModCN/blob/master/Screenshot/diytab.jpg)


