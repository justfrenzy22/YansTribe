import { IUser } from "@/types/IUser";
import { ApiService } from "../apiService";
import { IBaseUser } from "@/types/IBaseUser";

const apiService = new ApiService();

const GetUserByUsername = async (
	username: string,
	cookie: string | null
): Promise<{
	message: string;
	status: number;
	user: IBaseUser;
	profile: IUser;
}> => {
	return apiService.request<{
		message: string;
		status: number;
		user: IBaseUser;
		profile: IUser;
	}>({
		endpoint: `/user/get_user_profile/${username}`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie },
	});
};

export default GetUserByUsername;