IF EXISTS (SELECT 1 FROM [user] WHERE user_id = 1 AND [role] = 'superadmin')
BEGIN
    SELECT * FROM [user];
END
ELSE IF EXISTS (SELECT 1 FROM [user] WHERE user_id = 1 AND [role] = 'admin')
BEGIN
    SELECT * FROM [user] WHERE [role] = 'user';
END
ELSE
BEGIN
    SELECT * FROM [user] WHERE 1 = 0;
END;