import { useReducer, useRef } from "react";
import { toast } from "sonner";
import createPost from "@/api/post/create-post";
import UseTokenHook from "@/hooks/contexts/useTokenHook";
import { ICreatePostState as State } from "@/types/post/CreatePost/ICreateFormState";
import { CreatePostActionType as Action } from "@/types/post/CreatePost/ICreatePostActionType";
import { IMediaPreview } from "@/types/post/CreatePost/IMediaPreview";
import { PostActionTypeEnum as ActionType } from "@/enums/ICreatePostActionSetType";

const initState: State = {
	isDrag: false,
	isLoading: false,
	isExpanded: false,
	formData: { content: "", files: [] },
};

const reducer = (state: State, action: Action): State => {
	switch (action.type) {
		case ActionType.SET_DRAG:
			return {
				...state,
				isDrag: action.payload,
			};
		case ActionType.SET_LOADING:
			return {
				...state,
				isLoading: action.payload,
			};
		case ActionType.SET_EXPANDED:
			return {
				...state,
				isExpanded: action.payload,
			};
		case ActionType.SET_FILES:
			return {
				...state,
				formData: { ...state.formData, files: action.payload },
			};
		case ActionType.SET_CONTENT:
			return {
				...state,
				formData: { ...state.formData, content: action.payload },
			};
		case ActionType.RESET_FORM:
			return { ...state, formData: initState.formData, isExpanded: false };
		default:
			return state;
	}
};

const createMediaPreview = (files: FileList): IMediaPreview[] =>
	Array.from(files).map((file) => ({
		type: file.type.startsWith("image/") ? "image" : "video",
		url: URL.createObjectURL(file),
		file,
	}));

const convertToFormData = (
	content: string,
	files: IMediaPreview[]
): FormData => {
	const formData = new FormData();
	formData.append("content", content);
	files.forEach((media) => formData.append("files", media.file));
	return formData;
};

const useCreatePost = () => {
	const [state, dispatch] = useReducer(reducer, initState);
	const fileInputRef = useRef<HTMLInputElement>(null);
	const textareaRef = useRef<HTMLTextAreaElement>(null);

	const handleFileInput = (files: FileList) => {
		const newFiles = createMediaPreview(files);
		dispatch({
			type: ActionType.SET_FILES,
			payload: [...state.formData.files, ...newFiles],
		});
	};

	const handleSubmit = async (e: React.FormEvent) => {
		e.preventDefault();
		if (!state.formData.content.trim() && !state.formData.files.length) return;

		dispatch({ type: ActionType.SET_LOADING, payload: true });
		try {
			const token = await UseTokenHook();
			const res = await createPost(
				token,
				convertToFormData(state.formData.content, state.formData.files)
			);

			if (res.status === 200)
				toast.success(res.message, {
					action: {
						label: "Ok",
						onClick: () => dispatch({ type: ActionType.RESET_FORM }),
					},
				});
			else toast.error(res.message);
		} catch (err) {
			toast.error(err instanceof Error ? err.message : "Something went wrong.");
		} finally {
			dispatch({ type: ActionType.SET_LOADING, payload: false });
		}
	};

	return {
		state,
		dispatch,
		handleFileInput,
		handleSubmit,
		fileInputRef,
		textareaRef,
	};
};

export default useCreatePost;
