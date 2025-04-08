import Link from "next/link";
import { Avatar, AvatarImage, AvatarFallback } from "../ui/avatar";
import { Card } from "../ui/card";

const Comment = ({}) => {
	return (
		<div className="flex flex-row gap-4">
			<Avatar className="w-9 h-9">
				<AvatarImage src={`../`} alt="User pic" />
				<AvatarFallback>
					YT
					{
						// alternative name
					}
				</AvatarFallback>
			</Avatar>
			<Card className="w-full flex px-4 py-2 flex-col gap-1 items-start justify-center bg-secondary rounded-xl">
				<Link
					href={`shmatka_model`}
					className="font-semibold hover:underline items-center cursor-pointer"
				>
					User Name
				</Link>
				<div className="text-muted-foreground">
					asdadsahdsadjsadsajdhasjdasj
				</div>
			</Card>
		</div>
	);
};

export default Comment;
