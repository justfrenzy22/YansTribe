import Link from "next/link";
import ToggleTheme from "../custom/toggle-theme";

const Header = () => {
	return (
		<header className="w-full p-4 flex justify-between items-center">
			<Link href="/" className="flex items-center gap-2">
				<div className="h-8 w-8 rounded-full bg-gradient-to-br from-blue-500 flex items-center justify-center text-white font-bold text-lg select-none">
					Y
				</div>
				<span className="hidden font-bold text-xl md:inline-block select-none">
					YansTribe
				</span>
			</Link>
			<ToggleTheme />
		</header>
	);
};


export default Header;