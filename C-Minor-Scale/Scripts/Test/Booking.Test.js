QUnit.test("FilterDesks", function (assert) {
    var beforeFilter = [{ Type: 'DESK' }, { Type: 'FLOOR' }, { Type: 'DESK' }, {}];
    var afterFilter = filterDesks(beforeFilter);
    
    assert.equal(afterFilter.length, 2);
    assert.equal(afterFilter[0].Type, 'DESK');
    assert.equal(afterFilter[1].Type, 'DESK');
});

QUnit.test("CompareDesks", function (assert) {
    var positive = compareDesks({ Name: 'Plats 14' }, { Name: 'Plats 1' });
    var negative = compareDesks({ Name: 'Plats 1' }, { Name: 'Plats 14' });
    var zero = compareDesks({ Name: 'Plats 2' }, { Name: 'Plats 2' });

    assert.ok(positive > 0, 'Positive is: ' + positive);
    assert.ok(negative < 0, 'Negative is: ' + negative);
    assert.equal(zero, 0, 'Zero is: ' + zero);
});

QUnit.test("ButtonTime", function (assert) {
    var timestamp = getButtonTime('2017-05-19', 8);

    assert.equal(timestamp, 1495173600000);
});

QUnit.test("Name", function (assert) {
    assert.ok(true);
});
