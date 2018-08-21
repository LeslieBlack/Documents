


/******************************************************************/

SELECT DISTINCT MOH.* 
FROM 
       OPENQUERY
       (
       CIMSF,' SELECT  
GDD.IN_DATE AS CIMS_DATE, 
SD.IN_DATE AS CIMS_INDATE,   
SD.SOR_REF AS DEMANDWARE_NUM, 
GID.GITEM_RANK AS ORDER_TYPE
FROM  
       PRO.GTRANS_DEFS GDD
INNER JOIN 
       PRO.GTRANS_ITEMS GID
       ON GID.GTRANS_NUM = GDD.GTRANS_NUM
JOIN 
       PRO.SOR_DEFS SD 
       ON GID.SOR_NUM = SD.SOR_NUM
JOIN 
       PRO.SOR_SUNDS SS 
       ON SD.SOR_NUM = SS.SOR_NUM
WHERE 
SD.IN_DATE > TO_DATE(''26/JUL/2018 10:00:00'',''DD/MON/YYYY HH24:MI:SS'') 
AND SD.IN_DATE < TO_DATE(''26/JUL/2018 11:00:00'',''DD/MON/YYYY HH24:MI:SS'')      

AND SD.SOR_REF IS NOT NULL
AND (SD.SOR_REF LIKE''%UKF%'' OR SD.SOR_REF LIKE''%USF%'')
ORDER BY 
SD.IN_DATE ASC' 
       ) MOH
WHERE NOT EXISTS (SELECT ORDER_NO FROM FF_ORDER O WHERE O.ORDER_NO = MOH.DEMANDWARE_NUM )


/********************************************************************************************/
--USF1




select * from ff_order where order_no = 'UKF1001358109'








