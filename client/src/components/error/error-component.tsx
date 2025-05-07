import { useAppContext } from "@/hooks/useAppContext";
import { Button } from "../ui/button";
import { redirect } from "next/navigation";

const ErrorComponent = () => {
	const context = useAppContext();

	return (
		<div>
			Error page
			<p>Something went wrong</p>
			<p>Try refreshing the page</p>
			<Button onClick={() => window.location.reload()}>Refresh</Button>
			or go back to the{" "}
			<Button
				onClick={() => {
					context?.setCurrPage({
						page: "home",
						username: "",
					});
					redirect("/");
				}}
			>
				home page
			</Button>
		</div>
	);
};

export default ErrorComponent;
