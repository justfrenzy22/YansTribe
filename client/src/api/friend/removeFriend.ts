"use server";
import IUserContext from "@/types/context/IProfileContext";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const removeFriend = async (
	user2_id: string,
	cookie: string | null
): Promise<{
	message: string;
	status: number;
	profile: IUserContext[`profile`];
}> =>
	apiService.request<{
		message: string;
		status: number;
		profile: IUserContext[`profile`];
	}>({
		endpoint: `/friend/remove/${user2_id}`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie },
	});

export default removeFriend;
