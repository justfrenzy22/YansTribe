import { ApiService } from "../apiService";

const apiService = new ApiService();

const register = async (
	username: string,
	email: string,
	password: string,
	full_name: string,
	bio: string,
	location: string,
	website: string
): Promise<{
	message: string;
	status: number;
}> =>
	apiService.request<{ message: string; status: number }>(
		`/user/register`,
		`POST`,
		{ username, email, password, full_name, bio, location, website },
		false
	);

export default register;