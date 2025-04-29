import { IBaseUser } from "@/types/IEssentialsUser";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const getUserById = async (
	cookie: string | null
): Promise<{
	message: string;
	status: number;
	user: IBaseUser;
}> =>
	apiService.request<{
		message: string;
		status: number;
		user: IBaseUser;
	}>({
		endpoint: `/user/get_user`,
		method: `GET`,
		options: { requiresAuth: true, cookie }, // Pass cookie in options
	});

export default getUserById;
