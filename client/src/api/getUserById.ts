import { IBaseUser } from "@/types/IEssentialsUser";
import { ApiService } from "./apiService";

const apiService = new ApiService();

const getUserById = async (): Promise<{
	message: string;
	status: number;
	user: IBaseUser;
}> =>
	apiService.request<{
		message: string;
		status: number;
		user: IBaseUser;
	}>(`user/getUserById`, `GET`, {}, true);

export default getUserById;
