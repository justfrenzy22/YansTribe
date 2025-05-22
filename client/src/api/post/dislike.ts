import { IResponse } from "@/types/IResponse";
import { ApiService } from "../apiService";

const apiService = new ApiService();

const dislikePost = async (
	postId: string,
	cookie: string | null
): Promise<IResponse> =>
	apiService.request<IResponse>({
		endpoint: `/post/dislike/${postId}`,
		method: `GET`,
		options: { requiresAuth: true, cookie: cookie },
	});

export default dislikePost;
