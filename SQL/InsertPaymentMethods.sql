--=========================
-- INSERT PaymentMethods --
--=========================

-- Insert Cash payment method if it does not exist
IF NOT EXISTS (
    SELECT 1 
    FROM PaymentMethods 
    WHERE [Name] = 'Cash'
)
BEGIN
    SET IDENTITY_INSERT PaymentMethods ON;

    INSERT INTO PaymentMethods (PaymentMethodId, [Name])
    VALUES (1, 'Cash');

    SET IDENTITY_INSERT PaymentMethods OFF;
END

-- Insert Credit Card payment method if it does not exist
IF NOT EXISTS (
    SELECT 1 
    FROM PaymentMethods 
    WHERE [Name] = 'Credit Card'
)
BEGIN
    SET IDENTITY_INSERT PaymentMethods ON;

    INSERT INTO PaymentMethods (PaymentMethodId, [Name])
    VALUES (2, 'Credit Card');

    SET IDENTITY_INSERT PaymentMethods OFF;
END

-- Insert Debit Card payment method if it does not exist
IF NOT EXISTS (
    SELECT 1 
    FROM PaymentMethods 
    WHERE [Name] = 'Debit Card'
)
BEGIN
    SET IDENTITY_INSERT PaymentMethods ON;

    INSERT INTO PaymentMethods (PaymentMethodId, [Name])
    VALUES (3, 'Debit Card');

    SET IDENTITY_INSERT PaymentMethods OFF;
END