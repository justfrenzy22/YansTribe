import { IBaseUser } from "../IBaseUser";
import { IUser } from "../IUser";

export default interface IProfileContext {
	user: IBaseUser | null;
	setProfile: React.Dispatch<React.SetStateAction<IUser | null>>;
	profile: IUser | null;
}
