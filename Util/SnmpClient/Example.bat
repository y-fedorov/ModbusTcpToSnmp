@echo off


set IP=127.0.0.1
set PORT=161
set config_json=Resources/modbus-snmp-orig2.json


rem --All parameters
snmpclient.exe --ip %IP% --port %PORT% --config %config_json%

rem --Filter by device
rem snmpclient.exe --ip %IP% --port %PORT% --config %config_json% --devices techmeh_pumpcontrol_rack

rem --Filter by channels
rem snmpclient.exe --ip %IP% --port %PORT% --config %config_json% --channels ust_vtorich_niz_sp ust_vtorich_verh_sp

pause