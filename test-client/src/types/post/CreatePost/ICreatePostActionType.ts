import { PostActionTypeEnum as ActionType } from "@/enums/ICreatePostActionSetType";
import { ICreatePostFormData as FormData } from "./ICreatePostFormData";

export type CreatePostActionType =
	| { type: ActionType.SET_DRAG; payload: boolean }
	| { type: ActionType.SET_LOADING; payload: boolean }
	| { type: ActionType.SET_EXPANDED; payload: boolean }
	| { type: ActionType.SET_FILES; payload: FormData[`files`] }
	| { type: ActionType.SET_CONTENT; payload: FormData[`content`] }
	| { type: ActionType.RESET_FORM };
