import { IBaseUser } from "../IBaseUser";

export default interface IUserContext {
	user: IBaseUser | null;
}
