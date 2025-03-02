create table user (
    user_id int identity(1,1) primary key,
    username nvarchar(50) not null unique,
    email nvarchar(100) not null unique,
    hash_password nvarchar(100) not null,
    bio nvarchar(255) null,
    [location] nvarchar(100) null,
    website nvarchar(255) null,
    is_private bit not null default 0,
    created_at datetime not null default getdate()
);

create table post (
    post_id int identity(1, 1) primary key,
    user_id int not null,
    title nvarchar(50) not null,
    description nvarchar(255) not null,
    photo_url nvarchar(255) not null,
    created_at datetime not null default getdate(),
    constraint fk_posts_users foreign key (user_id) references users(user_id)
);

create table like (
    user_id int not null,
    post_id int not null,
    created_at datetime not null default getdate(),
    constraint pk_likes primary key (user_id, post_id),
    constraint fk_likes_usr foreign key (user_id) references users(user_id),
    constraint fk_likes_pst foreign key (post_id) references posts(post_id)
);

create table comment (
    comment_id int identity(1, 1) primary key,
    post_id int not null,
    user_id int not null,
    parent_id int null,
    content nvarchar(500) not null,
    created_at datetime not null default getdate(),
    constraint fk_comments_pst foreign key (post_id) references posts(post_id),
    constraint fk_comments_usr foreign key (user_id) references users(user_id),
    constraint fk_comments_prnt foreign key (parent_id) references comments(comment_id)
);

create table friend (
    user_id int not null,
    friend_id int not null,
    status nvarchar(50) not null default 'pending', -- Allowed values: 'pending', 'accepted', 'rejected',
    created_at datetime not null default getdate(),
    constraint pk_friends primary key (user_id, friend_id),
    constraint ck_status check (status in ('pending', 'accepted', 'rejected', 'blocked')),
    constraint fk_friends_user foreign key (user_id) references users(user_id),
    constraint fk_friends_friend foreign key (friend_id) references users(user_id)
);

create table chat (
    chat_id int identity(1, 1) primary key,
    user1_id int not null,
    user2_id int not null,
    created_at datetime not null default getdate(),
    constraint fk_chats_user1 foreign key (user1_id) references users(user_id),
    constraint fk_chats_user2 foreign key (user2_id) references users(user_id)
);

create table message (
    message_id int identity(1, 1) primary key,
    chat_id int not null,
    sender_id int not null,
    receiver_id int not null,
    content nvarchar(255) not null,
    send_at datetime not null default getdate(),
    constraint fk_messages_chat foreign key (chat_id) references chats(chat_id),
    constraint fk_messages_sender foreign key (sender_id) references users(user_id),
    constraint fk_messages_receiver foreign key (receiver_id) references users(user_id)
);

create table story (
    story_id int identity(1, 1) primary key,
    user_id int not null,
    media_url nvarchar(255) not null,
    caption nvarchar(500) not null,
    created_at datetime not null default getdate(),
    expires_at datetime not null,
    constraint fk_stories_user foreign key (user_id) references users(user_id),
    constraint ck_stories_expires check (expires_at > created_at)
);

