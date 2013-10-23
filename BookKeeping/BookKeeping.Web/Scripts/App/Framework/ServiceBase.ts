module Framework {
	export class ServiceBase {
		public ajaxRequest(type: string, url: string, data: any,
			callback = (items: any[]) => { },
			errorCallback = (exData: string) => { }): JQueryPromise<any> {
			var options = {
				dataType: "json",
				contentType: "application/json",
				cache: false,
				type: type,
				data: isString(data) ? data : JSON.stringify(data)
			};
			function toUrlParams(data: Object): string {
				var result = "1=1&";
				for (var k in data) {
					result += (encodeURIComponent(k) + "=" + encodeURIComponent(data[k] + "") + "&");
				}
				return result;
			}
			 
			console.log("Requesting " + url + " with " + options.data + "...");

			return $.ajax(url, options)
				.done((data: any, statusMessage: string, response: JQueryXHR) => {
					console.log("Success: Status code " + response.status + "(" + response.statusText + ") when requesting " + url);
					if (isArray(data))
						callback((<Array>data).map(item => this.propertiesToCamel(item)));
					else if (isString(data))
						callback([data]);
					else
						callback([this.propertiesToCamel(data)]);
				})
				.fail((response: JQueryXHR) => {
					console.log("Fail: Status code " + response.status + "(" + response.statusText + ") when requesting " + url);
					errorCallback(response.responseBody);
				});

			function isArray(obj) { return Object.prototype.toString.call(obj) === "[object Array]"; }
			function isString(obj) { return Object.prototype.toString.call(obj) == '[object String]'; }
		}

		public propertiesToCamel(obj: any): Object {
			if (!isObject(obj))
				return obj;

			var result = {};
			for (var k in obj) {
				var property = obj[k];
				result[toCamel(k)] = isObject(property)
					? this.propertiesToCamel(property)
					: isArray(property)
						? (<any[]>property).map(o => this.propertiesToCamel(o))
						: property;
			}
			return result;

			function toCamel(key: string): string {
				if (!key)
					return "";

				if (key.length == 1)
					return key.toLowerCase();

				var start = key.substr(0, 1);
				var end = key.substr(1);
				return start.toLowerCase() + end;
			}
			function isObject(o) { return o && o.constructor === Object; }
			function isArray(o) { return o && o.constructor === Array; }
		}
	}
}