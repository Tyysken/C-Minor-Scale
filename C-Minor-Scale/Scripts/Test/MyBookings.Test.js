QUnit.test("SortByStart", function (assert) {
    a = {};
    b = {};
    c = {};
    a.From = 1;
    b.From = 2;
    c.From = 2;
    assert.equal(SortByStart(a, b), -1);
    assert.equal(SortByStart(b, a), 1);
    assert.equal(SortByStart(b, c), 0);
});

QUnit.test("Name", function (assert) {
    assert.ok(true);
});
