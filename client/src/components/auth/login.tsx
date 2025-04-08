import { useRouter } from "next/navigation";
import { ILoginForm } from "@/types/ILoginForm";
import { useEffect, useRef, useState } from "react";
import { motion } from "framer-motion";
import { Label } from "../ui/label";
import { Eye, EyeOff, Loader2, Lock, Mail, Router } from "lucide-react";
import { Input } from "../ui/input";
import Link from "next/link";
import { Checkbox } from "../ui/checkbox";
import { Button } from "../ui/button";
import Error from "next/error";
import { ILoginErrors } from "@/types/ILoginErrors";
import { ApiService } from "@/api/auth/apiService";
import { toast } from "sonner";
import { Toaster } from "../ui/sonner";

const Login = ({ isLogin }: { isLogin: boolean }) => {
	const service = new ApiService();
	const router = useRouter();
	const [isLoading, setLoading] = useState<boolean>(false);
	const [formData, setFormData] = useState<ILoginForm>({
		email: "",
		password: "",
		rememberMe: false,
	});
	const [errors, setErrors] = useState<ILoginErrors>({} as ILoginErrors);
	const [showPassword, setShowPassword] = useState<boolean>(false);
	const emailInputRef = useRef<HTMLInputElement | null>(null);

	useEffect(() => {
		if (isLogin) {
			if (emailInputRef.current) {
				emailInputRef.current.focus();
			}
		}
	}, [isLogin]);

	const validateEmailFormat = (email: string) => {
		const pattern = /[a-z0-9]+@[a-z0-9]+\.[a-z]{2,3}/;
		return pattern.test(email);
	};

	const validate = (): boolean => {
		const newErrors: Record<string, string> = {};

		if (formData.email.trim() === "") {
			newErrors.email = `Email is required`;
		} else if (!validateEmailFormat(formData.email)) {
			newErrors.email = "Wrong email format";
		}

		if (formData.password.trim() === "") {
			newErrors.password = `Password is required`;
		}

		setErrors(newErrors);
		return Object.keys(newErrors).length === 0;
	};

	const handleSubmit = async (e: React.FormEvent) => {
		// const router = useRouter();
		e.preventDefault();

		if (!validate()) return;

		setLoading(true);
		console.log(`email`, formData.email, `password`, formData.password);

		try {
			const res = await service.login(formData.email, formData.password);
			// if (res.status === 200) {
			console.log(res);
			toast(res.message, {
				description: res.message,
				action: {
					label: "Ok",
					onClick: () => router.push("/"),
				},
			});

			// router.push("/");
			// document.cookie = `token=your-token; path=/; Secure; HttpOnly; SameSite=None`;
			// document.cookie = `token=${res.token}; path=/; Secure; HttpOnly; SameSite=None`;
			// display the message
			// wait for a bit (500ms)
			// router.push("/");
			// } else {
			// setErrors({ form: `Invalid email or password` });
			// setErrors({ form: res.message });
			// }
		} catch (err: Error | any) {
			setErrors({ form: err });
		} finally {
			setLoading(false);
		}
	};

	return (
		<form onSubmit={handleSubmit} className="space-y-4">
			{errors.form && (
				<motion.div
					initial={{ opacity: 0, y: -10 }}
					animate={{ opacity: 1, y: 0 }}
					className="p-3 rounded-md bg-red-50 dark:bg-red-900/20 text-red-500 dark:text-red-400 text-sm"
				>
					{errors.form}
				</motion.div>
			)}

			{/* Email input */}
			<div className="space-y-2">
				<Label htmlFor="email">Email</Label>
				<div className="relative">
					<Mail className="absolute left-3 top-3 h-4 w-4 text-gray-400" />
					<Input
						id="email"
						name="email"
						type="email"
						ref={emailInputRef}
						tabIndex={1}
						placeholder={`your@email.com`}
						className={`pl-10 border outline shadow-md rounded-lg bg-secondary ${
							errors.email ? `border-red-500 focus-visible:ring-red-500` : ``
						}`}
						value={formData.email}
						onChange={(e) =>
							setFormData({ ...formData, email: e.target.value })
						}
						disabled={isLoading}
					/>
				</div>
				{errors.email && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.email}
					</motion.p>
				)}
			</div>

			{/* Password input */}
			<div className="space-y-2">
				<div className="flex justify-between items-center">
					<Label htmlFor="password">Password</Label>
					<Link
						href={`/auth/forgot-password`}
						tabIndex={3}
						className="text-sm text-blue-600 hover:text-blue-800 hover:underline"
					>
						forgot your password?
					</Link>
				</div>
				<div className="relative">
					<Lock className="absolute left-3 top-3 h-4 w-4 text-gray-400" />
					{/* <div className="flex w-full flex-row items-center space-x-2"> */}
					<Input
						id="password"
						name="password"
						type={showPassword ? `text` : `password`}
						placeholder="password"
						tabIndex={2}
						className={`pl-10 border outline shadow-md w-full rounded-lg bg-secondary
							${errors.password ? `border-red-500 focus-visible:ring-red-500` : ``}`}
						value={formData.password}
						onChange={(e) =>
							setFormData({ ...formData, password: e.target.value })
						}
						disabled={isLoading}
					/>
					<Button
						type="button"
						tabIndex={4}
						onClick={() => setShowPassword(!showPassword)}
						className="absolute right-2 bottom-0 cursor-pointer rounded-full p-2 border-none bg-transparent hover:bg-transparent"
						variant={`ghost`}
					>
						{showPassword ? <Eye /> : <EyeOff />}
					</Button>
				</div>
				{errors.password && (
					<motion.p
						initial={{ opacity: 0, y: -10 }}
						animate={{ opacity: 1, y: 0 }}
						className="text-sm text-red-500"
					>
						{errors.password}
					</motion.p>
				)}
			</div>

			{/* Checkbox */}
			<div className="flex items-center space-x-2">
				<Checkbox
					id="remember-me"
					checked={formData.rememberMe}
					tabIndex={5}
					className="border outline shadow-md"
					onCheckedChange={(e) =>
						setFormData({ ...formData, rememberMe: e as boolean })
					}
					disabled={isLoading}
				/>
				<Label htmlFor="remember-me">Remember me</Label>
			</div>

			{/* Submit button */}
			<motion.div
				whileHover={{ scale: isLoading ? 1 : 1.02 }}
				whileTap={{ scale: isLoading ? 1 : 0.98 }}
			>
				<Button
					color="primary"
					type="submit"
					tabIndex={6}
					className="w-full cursor-pointer rounded-xl shadow-xl"
					disabled={isLoading}
				>
					{isLoading ? (
						<>
							<Loader2 className="mr-2 h-4 w-4 animate-spin" />
							Signing In...
						</>
					) : (
						`Sign in`
					)}
				</Button>
			</motion.div>

			{/* <Button
				color="primary"
				onClick={() => changeColorTheme("default", theme as string)}
			>
				Default
			</Button>
			<Button
				color="primary"
				onClick={() => changeColorTheme("red", theme as string)}
			>
				Red
			</Button> */}
			<Toaster position="top-right" />
		</form>
	);
};

export default Login;
