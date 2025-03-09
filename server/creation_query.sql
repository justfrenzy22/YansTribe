
create table user (
    user_id int identity(1, 1) primary key,
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

create table post (
    post_id int identity(1, 1) primary key,
    user_id int not null,
    title nvarchar(50) not null,
    has_img bit not null default 0,
    content nvarchar(max) not null,
    -- visibility
    created_at datetime not null default getdate(),
    constraint fk_post_user foreign key (user_id) references user(user_id)
);

create table like (
    user_id int not null,
    post_id int not null,
    issuer_id int not null,
    created_at datetime not null default getdate(),
    constraint pk_like primary key (user_id, post_id),
    constraint fk_like_user foreign key (user_id) references user(user_id),
    constraint fk_like_post foreign key (post_id) references post(post_id),
    constraint fk_like_issuer foreign key (issuer_id) references user(user_id)
);

create table comment (
    comment_id int identity(1, 1) primary key,
    post_id int not null,
    user_id int not null,
    parent_id int null,
    content nvarchar(500) not null,
    created_at datetime not null default getdate(),
    constraint fk_comment_post foreign key (post_id) references post(post_id),
    constraint fk_comment_user foreign key (user_id) references user(user_id),
    constraint fk_comment_parent foreign key (parent_id) references comment(comment_id)
);

create table friend (
    friendship_id int identity(1, 1) primary key,
    user_1_id int not null,
    user_2_id int not null,
    status nvarchar(50) not null default 'pending' check (status in ('pending', 'accepted', 'rejected', 'blocked')), -- Allowed values: 'pending', 'accepted', 'rejected',
    created_at datetime not null default getdate(),
    constraint friend_user_1 foreign key (user_1_id) references user(user_id),
    constraint friend_user_2 foreign key (user_2_id) references user(user_id)
);

create table chat (
    chat_id int identity(1, 1) primary key,
    user_1_id int not null,
    user_2_id int not null,
    created_at datetime not null default getdate(),
    constraint fk_chat_user_1 foreign key (user_1_id) references user(user_id),
    constraint fk_chat_user_2 foreign key (user_2_id) references user(user_id)
);

create table message (
    message_id int identity(1, 1) primary key,
    chat_id int not null,
    sender_id int not null,
    receiver_id int not null,
    content nvarchar(max) not null,
    send_at datetime not null default getdate(),
    constraint fk_message_chat foreign key (chat_id) references chat(chat_id),
    constraint fk_message_sender foreign key (sender_id) references user(user_id),
    constraint fk_message_receiver foreign key (receiver_id) references user(user_id)
);

create table story (
    story_id int identity(1, 1) primary key,
    user_id int not null,
    media_type nvarchar(20) not null check (media_type in ('image', 'video')),    
    media_url nvarchar(255) not null,
    caption nvarchar(500) not null,
    created_at datetime not null default getdate(),
    expires_at datetime not null,
    constraint fk_story_user foreign key (user_id) references user(user_id),
    constraint ck_story_expires check (expires_at > created_at)
);