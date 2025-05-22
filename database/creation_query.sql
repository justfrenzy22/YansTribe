create table [user]
(
    user_id uniqueidentifier primary key default newid(),
    username nvarchar(50) not null unique,
    email nvarchar(100) not null unique,
    password_hash nvarchar(100) not null,
    full_name nvarchar(100) not null,
    bio nvarchar(255) null,
    pfp_src nvarchar(255) null,
    [location] nvarchar(100) null,
    website nvarchar(255) null,
    is_private bit not null default 0,
    created_at datetime not null default getdate(),
    [role] nvarchar(20) default 'user' check ([role] in ('user', 'admin', 'superadmin'))
);

create table post
(
    post_id uniqueidentifier primary key default newid(),
    user_id uniqueidentifier not null,
    content nvarchar(max) not null,
    edited bit not null default 0,
    edited_at datetime null,
    created_at datetime not null default getdate(),
    constraint fk_post_user foreign key (user_id) references [user](user_id)
);

create table post_like
(
    user_id uniqueidentifier not null,
    post_id uniqueidentifier not null,
    created_at datetime not null default getdate(),
    constraint pk_post_like primary key (user_id, post_id),
    constraint fk_post_like_user foreign key (user_id) references [user](user_id),
    constraint fk_post_like_post foreign key (post_id) references post(post_id)
);

-- In the future
-- //TODO Add tags
-- create table post_tag ()

create table post_media
(
    media_id uniqueidentifier primary key default newid(),
    post_id uniqueidentifier not null,
    media_type nvarchar(20) not null check (media_type in ('image', 'video')),
    media_src nvarchar(255) not null,
    [order] int not null default 0,
    constraint fk_post_media_post foreign key (post_id) references post(post_id)
)

create table comment
(
    comment_id uniqueidentifier primary key default newid(),
    post_id uniqueidentifier not null,
    user_id uniqueidentifier not null,
    parent_id uniqueidentifier null,
    content nvarchar(500) not null,
    created_at datetime not null default getdate(),
    edited bit not null default 0,
    edited_at datetime null,
    constraint fk_comment_post foreign key (post_id) references post(post_id),
    constraint fk_comment_user foreign key (user_id) references [user](user_id),
    constraint fk_comment_parent foreign key (parent_id) references comment(comment_id)
);

create table comment_like
(
    user_id uniqueidentifier not null,
    comment_id uniqueidentifier not null,
    created_at datetime not null default getdate(),
    constraint pk_comment_like primary key (user_id, comment_id),
    constraint fk_comment_like_user foreign key (user_id) references [user](user_id),
    constraint fk_comment_like_comment foreign key (comment_id) references comment(comment_id)
);

create table friend
(
    friendship_id uniqueidentifier primary key default newid(),
    user_1_id uniqueidentifier not null,
    user_2_id uniqueidentifier not null,
    status nvarchar(50) not null default 'pending' check (status in ('pending', 'accepted', 'rejected', 'blocked')),
    -- Allowed values: 'pending', 'accepted', 'rejected', 'blocked'
    created_at datetime not null default getdate(),
    constraint friend_user_1 foreign key (user_1_id) references [user](user_id),
    constraint friend_user_2 foreign key (user_2_id) references [user](user_id)
);

create table chat
(
    chat_id uniqueidentifier primary key default newid(),
    user_1_id uniqueidentifier not null,
    user_2_id uniqueidentifier not null,
    created_at datetime not null default getdate(),
    constraint fk_chat_user_1 foreign key (user_1_id) references [user](user_id),
    constraint fk_chat_user_2 foreign key (user_2_id) references [user](user_id)
);

create table message
(
    message_id uniqueidentifier primary key default newid(),
    chat_id uniqueidentifier not null,
    sender_id uniqueidentifier not null,
    content nvarchar(max) not null,
    send_at datetime not null default getdate(),
    constraint fk_message_chat foreign key (chat_id) references chat(chat_id),
    constraint fk_message_sender foreign key (sender_id) references [user](user_id),
);

create table story
(
    story_id uniqueidentifier primary key default newid(),
    user_id uniqueidentifier not null,
    media_type nvarchar(20) not null check (media_type in ('image', 'video')),
    media_src nvarchar(255) not null,
    caption nvarchar(500) not null,
    created_at datetime not null default getdate(),
    expires_at datetime not null,
    constraint fk_story_user foreign key (user_id) references [user](user_id),
    constraint ck_story_expires check (expires_at > created_at)
);

-- CREATE TABLE WebsiteSessions (
-- 	Id UNIQUEIDENTIFIER PRIMARY KEY,
-- 	UserAgent NVARCHAR(1024),
-- 	IPAddress NVARCHAR(45),
-- 	CreatedAt DATETIME
-- );