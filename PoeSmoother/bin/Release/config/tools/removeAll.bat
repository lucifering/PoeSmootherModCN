REM Use at your own risk, it does a mass DELETE of everything!

SET /p ExcludeFiles=What file type should be kept (NOT deleted)? Type the file name(s) inside parantheses. example: (pdf) or (shp dbf shx)     
SET /p MapDrive=What drive letter is the folder in? example: c or n     
SET /p Directory=Drag the folder you would like to modify into this command prompt then press ENTER.     

%MapDrive%:
cd %Directory%

attrib +a *.* /s
echo %date%
for %%i in %ExcludeFiles% do attrib -a *.%%i /s
echo %date%
del %Directory%\*.* /s /a:a /q

echo %date%
attrib +a %Directory%\*.* /s
echo %date%