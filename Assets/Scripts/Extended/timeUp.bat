w32tm /config /manualpeerlist:time.windows.com /syncfromflags:manual /reliable:yes
w32tm /config /update 
net stop w32time && net start w32time 