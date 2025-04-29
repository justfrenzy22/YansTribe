import { MediaPreview } from "./IMediaPreview";

export interface CreatePostFormData {
	content: string;
	files: MediaPreview[];
}

export interface ConvPostFormData {
	content: string;
	files: File[];
}
