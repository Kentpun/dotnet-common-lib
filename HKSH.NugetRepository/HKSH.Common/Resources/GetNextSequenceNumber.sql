-- ------------------------------------------------------------ --
--                                                              --
-- This Sql Script is meant to be executed directly on the      --
-- Database, it's supposed to replace a stored procedure,       --
-- so it can be updated without Migrations.                     --
--                                                              --
-- The following parameters are required:                       --
--                                                              --
--  @Category string,                                           --
--  @DependentSymbol string,                                    --
--  @StartingNumber decimal,                                    --
--  @PaddingCount int                                           --
--                                                              --
--  returns: string                                             --
--                                                              --
-- ------------------------------------------------------------ --

BEGIN TRAN

DECLARE @NextSymbol nvarchar(280)
DECLARE @NextNumberTable table (NextNumber decimal(18))
DECLARE @NextNumber decimal(18)

IF NOT EXISTS (SELECT Category FROM [dbo].common_NumberSequence WHERE Category = @Category)
BEGIN
    INSERT INTO common_NumberSequence  (Category, DependentSymbol, LastRequestedNumber)
    VALUES                      (@Category, @DependentSymbol, @StartingNumber)
END

UPDATE  [dbo].common_NumberSequence
SET     [LastRequestedNumber] = [LastRequestedNumber] + CONVERT(decimal(18), 1)
OUTPUT  INSERTED.[LastRequestedNumber] INTO @NextNumberTable
WHERE   [DependentSymbol] = @DependentSymbol AND [Category] = @Category

SELECT  @NextNumber = NextNumber FROM @NextNumberTable

IF @NextNumber IS NULL
BEGIN
    UPDATE  [dbo].common_NumberSequence
    SET     [LastRequestedNumber] = @StartingNumber, [DependentSymbol] = @DependentSymbol
    WHERE   [Category] = @Category
    
    SET @NextSymbol = @DependentSymbol + RIGHT('000000000000000000' + CONVERT(nvarchar(18), @StartingNumber), @PaddingCount)
END
ELSE
BEGIN
    SET @NextSymbol = @DependentSymbol + RIGHT('000000000000000000' + CONVERT(nvarchar(18), @NextNumber), @PaddingCount)
END

SELECT @NextSymbol as NextSymbol

COMMIT TRAN