import { IMediaPreview } from "./IMediaPreview";

export interface ICreatePostFormData {
	content: string;
	files: IMediaPreview[];
}

export interface ConvPostFormData {
	content: string;
	files: File[];
}
