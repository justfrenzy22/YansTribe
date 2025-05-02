import { ICreatePostFormData as FormData } from "@/types/post/CreatePost/ICreatePostFormData";

export interface ICreatePostState {
	isDrag: boolean;
	isLoading: boolean;
	isExpanded: boolean;
	formData: FormData;
}
