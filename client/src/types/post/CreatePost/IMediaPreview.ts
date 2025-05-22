export type IMediaPreview = {
	type: "image" | "video";
	url: string;
	file: File;
};