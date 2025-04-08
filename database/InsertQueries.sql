
-- Insert users
INSERT INTO [user] (username, email, full_name, password_hash, created_at) VALUES
('UserOne', 'user1@example.com', 'UserOne' ,'hashed_password_1', '2024-10-01 08:00:00'), -- Gets user_id 1 (usually)
('UserTwo', 'user2@example.com', 'UserTwo' ,'hashed_password_2', '2024-10-02 09:00:00'), -- Gets user_id 2
('UserThree', 'user3@example.com', 'UserThree', 'hashed_password_3', '2024-10-03 10:00:00'), -- Gets user_id 3
('UserFour', 'user4@example.com', 'UserFour', 'hashed_password_4', '2024-10-04 11:00:00'), -- Gets user_id 4
('UserFive', 'user5@example.com', 'UserFive', 'hashed_password_5', '2024-10-05 12:00:00'); -- Gets user_id 5

-- Insrt friendships
INSERT INTO friend (friendship_id, user_1_id, user_2_id, status, created_at) VALUES
(1, (SELECT user_id from [user] where user_id = 1), (SELECT user_id from [user] where user_id = 3), 'accepted', '2024-10-15 10:00:00'),
(3, (SELECT user_id from [user] where user_id = 1), (SELECT user_id from [user] where user_id = 4), 'accepted', '2024-11-20 11:30:00');



-- Insert posts
INSERT INTO post (user_id, title, has_img, media_src, content, created_at) VALUES
-- id 3
((SELECT user_id from [user] where user_id = 3), 'This is a post by UserThree', 0, null, 'This is a post by UserThree', '2024-10-03 10:00:00'),
((SELECT user_id from [user] where user_id = 3), 'Masduh asjdkhj n asiujkh 12', 1, 'https://example.com/image.jpg', 'jkasndjiukhsadjksanb na sd snajkd nsajkdnsakjdnsajk dnsajk njks njksadnjksadnjkasdnjksan dasjkn dkjasdnkjasn', '2024-10-03 10:00:00'),
((SELECT user_id from [user] where user_id = 3), 'aushydb123 ansjdbg 3najhbsdbjha', 2, 'http://example.com/image1.jpg', 'jansbdjhsabjheb qwneiuashdkjbaskjdnkjasnd', '2024-10-03 10:00:00'),
-- id 4
((SELECT user_id from [user] where user_id = 4), 'This is a post by UserFour', 0, null, 'This is a post by UserFour', '2024-10-04 11:00:00'),
((SELECT user_id from [user] where user_id =4), 'Thats some cool post by me', 1, 'https://example.com/image2.jpg', 'This is a post by UserFour', '2024-10-04 11:00:00'),
((SELECT user_id from [user] where user_id =4), 'I love this post', 2, 'https://example.com/image3.jpg', 'This is a post by UserFoukasdjdhadhakdajksdhjkahdjkashkdjhasjkdr', '2024-10-04 11:00:00');



-- Insert post likes
INSERT INTO post_like (post_id, user_id, created_at) VALUES
(1, (SELECT user_id from [user] where user_id = 3), '2024-10-03 10:00:00'),
(2, (SELECT user_id from [user] where user_id = 4), '2024-10-04 11:00:00');



-- Insert comments
INSERT INTO comment (post_id, user_id, parent_id, content, created_at) VALUES
((SELECT post_id from post where post_id = 1), (SELECT user_id from [user] where user_id = 1), null, 'This is a comment by UserFour', '2024-10-05 12:00:00'),
((SELECT post_id from post where post_id = 1), (SELECT user_id from [user] where user_id = 2), null, 'That thing is funny', '2024-10-05 12:00:00'),
((SELECT post_id from post where post_id = 2), (SELECT user_id from [user] where user_id = 4), null, 'This is a comment by UserThree', '2024-10-05 12:00:00');



-- Insert comment likes
INSERT INTO comment_like (user_id, comment_id, created_at) VALUES
((SELECT user_id from [user] where user_id = 3), (SELECT comment_id from comment where comment_id = 3), '2024-10-03 10:00:00'),
((SELECT user_id from [user] where user_id = 4),(SELECT comment_id from comment where comment_id = 4), '2024-10-04 11:00:00'),
((SELECT user_id from [user] where user_id = 4),(SELECT comment_id from comment where comment_id = 4), '2024-10-04 11:00:00');



-- INSERT INTO post (post_id, user_id, content, created_at) VALUES
-- (1, (SELECT user_id from [user] where user_id = 1), 'This is a post by UserOne', '2024-10-01 08:00:00'),
-- (2, (SELECT user_id from [user] where user_id = 2), 'This is a post by UserTwo', '2024-10-02 09:00:00'),
-- (3, (SELECT user_id from [user] where user_id = 3), 'This is a post by UserThree', '2024-10-03 10:00:00'),
-- (4, (SELECT user_id from [user] where user_id = 4), 'This is a post by UserFour', '2024-10-04 11:00:00'),


