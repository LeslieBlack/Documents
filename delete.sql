
/*****************************************************************************************************************
 Delete in batches of 2000 rows, each batch  will be committed in its own transaction 
 if the job fails or needs to be stopped previous  batched  have been committed to on restart it doesn’t have to 
 begin all over again. this approach should also reduce ongoing impact on transaction logs 
 *****************************************************************************************************************/

DECLARE @DR INT;
SET @DR = 1;

WHILE (@DR > 0)
  BEGIN
  BEGIN TRANSACTION;
     DELETE TOP (2000)  FROM T_AUDIT 
     WHERE RECORD_CREATE_TIME < (GETDATE()-30)
  SET @DR = @@ROWCOUNT;
  COMMIT TRANSACTION;
END 

