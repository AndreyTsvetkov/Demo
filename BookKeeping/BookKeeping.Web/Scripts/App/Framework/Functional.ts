module Framework {
	export module Functional {
		export function groupBy<T, K>(array: T[], keySelector: (arg: T) => K) {
			var mapped = array.map(i => { return { key: keySelector(i), value: i }; } );
			return <{ key: K; values: T[] }[]>mapped.reduce((acc: { key: K; values: T[] }[], item: { key: K; value: T }, index, array) => {
				var byKey = acc.filter(i => i.key === item.key);

				if (byKey.length > 0)
					byKey[0].values.push(item.value);
				else
					acc.push({ key: item.key, values: [item.value] });

				return acc;
			}, []);
		}

		export function selectMany<T, O>(array: T[], projection: (arg: T) => O[]) : O[] {
			return array.reduce((acc, item, index, arr) => { projection(item).forEach(i => acc.push(i)); return acc;}, <O[]>[]);
		}
	} 
}