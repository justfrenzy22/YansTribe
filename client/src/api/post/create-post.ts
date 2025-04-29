// import { ConvPostFormData } from "@/types/ICreatePostFormData";
// import { CreatePostFormData } from "@/types/ICreatePostFormData";
import { ApiService } from "../apiService";

const apiService = new ApiService();

// interface CustomFormData extends FormData {
// 	files?: File[];
// 	content: string;
// }

const createPost = async (
	cookie: string | null,
	form: FormData
): Promise<{
	message: string;
	status: number;
}> => {
	// const formData = new FormData();
	// formData.append("content", body.content);

	// body.files.forEach((file) => {
	// 	formData.append("files", file.file);
	// });

	return await apiService.request<{ message: string; status: number }>({
		endpoint: `/post/create_post`,
		method: `POST`,
		options: {
			form: form,
			files: form.getAll("files").length > 0,
			requiresAuth: true,
			cookie: cookie,
		},
	});
};

export default createPost;
