import { ApiService } from "../apiService";

const apiService = new ApiService();

const login = async (
	email: string,
	password: string
): Promise<{
	message: string;
	status: number;
	token?: string;
}> =>
	apiService.request<{ message: string; status: number; token?: string }>({
		endpoint: `/user/login`,
		method: `POST`,
		options: { body: { email, password }, requiresAuth: true },
	});

export default login;
