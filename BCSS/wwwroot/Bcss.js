function checkcss(propname, propvalue) {
    var result = CSS.supports(propname, propvalue);
    return result;
}
