module Framework {
	export class DateUtil {
		public static format(date: Date, format: string): string {
			return format
				.replace("YYYY", padStr(date.getFullYear()))
				.replace("MM", padStr(1 + date.getMonth()))
				.replace("DD", padStr(date.getDate()))
				.replace("hh", padStr(date.getHours()))
				.replace("mm", padStr(date.getMinutes()))
				.replace("ss", padStr(date.getSeconds()));
 
			function padStr(i) {
				return (i < 10) ? "0" + i : "" + i;
			}
		}

		public static now(): Date {
			return new Date();
		}

		public static dayBefore(): Date {
			return DateUtil.addDays(DateUtil.now(), -1);
		}
		public static dayAfter(): Date {
			return DateUtil.addDays(DateUtil.now(), 1);
		}

		public static addDays(date: Date, count: number): Date {
			var oneDayInMs = 86400000;
			var ms = date.getTime() + oneDayInMs * count;
			return new Date(ms);
		}

		public static fromISO(isoDate: string): Date {
			return new Date(isoDate);
		}
	}
}