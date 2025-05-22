import { useAppContext } from "@/hooks/contexts/useAppContext";
import { Button } from "../ui/button";
import { redirect } from "next/navigation";
import { Navbar } from "../custom/navbar";

const ErrorComponent = ({ message }: { message: string }) => {
	const context = useAppContext();

	return (
		<div>
			<Navbar />
			<div className="flex flex-col gap-4 w-full h-screen items-center justify-center">
				Error page
				<p className="text-red-500">{message}</p>
				<p>Try refreshing the page</p>
				<Button variant={`destructive`} className="rounded-xl cursor-pointer" onClick={() => window.location.reload()}>Refresh</Button>
				or go back to the{" "}
				<Button
					className="bg-primary/50 rounded-xl cursor-pointer"
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
		</div>
	);
};

export default ErrorComponent;
