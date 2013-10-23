var Framework;
(function (Framework) {
    (function (Functional) {
        function groupBy(array, keySelector) {
            var mapped = array.map(function (i) {
                return { key: keySelector(i), value: i };
            });
            return mapped.reduce(function (acc, item, index, array) {
                var byKey = acc.filter(function (i) {
                    return i.key === item.key;
                });

                if (byKey.length > 0)
                    byKey[0].values.push(item.value);
else
                    acc.push({ key: item.key, values: [item.value] });

                return acc;
            }, []);
        }
        Functional.groupBy = groupBy;

        function selectMany(array, projection) {
            return array.reduce(function (acc, item, index, arr) {
                projection(item).forEach(function (i) {
                    return acc.push(i);
                });
                return acc;
            }, []);
        }
        Functional.selectMany = selectMany;
    })(Framework.Functional || (Framework.Functional = {}));
    var Functional = Framework.Functional;
})(Framework || (Framework = {}));
//# sourceMappingURL=Functional.js.map
