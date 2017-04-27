# PoeSmootherModCN #
===================================  
Chinese version of PoeSmoother, with MOD special effects replacement function

Download:
https://github.com/lucifering/PoeSmootherModCN/archive/master.zip

(The integrated development environment is：Visual Studio 2015.  program file is below \bin\Release)

OR：  

http://pan.baidu.com/s/1mi7bSqG 


  
  

Project based PoeSmoother：https://github.com/thisTwo/PoeSmoother
__________________________________________________________

**Program requires Microsoft.NET Framework 4.5 runtime environment.**  

**Remember to back up your Client.ggpk before starting to change anything.**  

**Be careful!Account may be banned!!!**  


***1.Back up your Content.ggpk.***  

***2.Satrt PoeSmoother.exe,Select your Content.ggpk file.***  

***3.Wait 100%：***  

<code>
GGPK: ****/Content.ggpk

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

>>>
Traversing tree....
All done!


</code>

***4.Check the checkbox for changes to take effect.To reset things to default,just check / uncheck the options twice.***  


***5.Enjoy***  




 remove effects:

![image](https://github.com/lucifering/PoeSmootherModCN/blob/master/Screenshot/2-2.jpg)


Custom MOD:

![image](https://github.com/lucifering/PoeSmootherModCN/blob/master/Screenshot/1-2.jpg)



![image](https://github.com/lucifering/PoeSmootherModCN/blob/master/Screenshot/3-2.jpg)


Click on the icon to switch languages(only simplified Chinese and English). 


My blog:**http://poetw.blog.163.com/** 


# Diy your Program  #
===================================    

In "cn" and "us" folder,there are 3 files: Effects.json(Effects Tab)  MOD.json(MOD Tab)  Skills.json(Skill Tab).

you  can modify the file to DIY your Program.

they all json format and UTF-8 encoding.


Example：
<code>
[
    {
        "Name": "- Title",
        "Items": [
            {               
                "Content": "- Item 1",              
                "ToolTip": "Item tool tip text",
                "Tag": {
                    "ToolTipImg": "/mod/preview image(jpg)",
                    "NewMOD": ["new mod file path"],
                    "OldMOD": ["restore default file path"]
                }
            }
        ]
    } 
]
</code>



![image](https://github.com/lucifering/PoeSmootherModCN/blob/master/Screenshot/diytab-2.jpg)







   
