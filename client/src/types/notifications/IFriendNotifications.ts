import { IBaseUser } from "../IBaseUser";

export interface IFriendNotifications {
    pfp_src: IBaseUser[`pfp_src`];
    sender_id: IBaseUser[`user_id`];
    username: IBaseUser[`username`];
    request_sent_at: Date;
}