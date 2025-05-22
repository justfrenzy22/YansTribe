import { ApiService } from "../apiService";
import { IUserResponse } from "@/types/IResponse";

const apiService = new ApiService();

const getUserById = async (cookie: string | null): Promise<IUserResponse> =>
	apiService.request<IUserResponse>({
		endpoint: `/user/get_user`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie }, // Pass cookie in options
	});

export default getUserById;
