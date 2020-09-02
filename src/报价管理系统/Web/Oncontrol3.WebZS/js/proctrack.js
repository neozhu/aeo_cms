
var TRACK_PAGEURL = '/CommonPages/ProcTrack.aspx';

function renderProcTrack(args) {
    var args = args || {};
    //var phase = (args.phase || '').toLowerCase();
    //args.tracktype = args.tracktype || $.getQueryString({ 'ID': 'tracktype' }) || 'Procurement';

    var trackurl = $.combineQueryUrl(TRACK_PAGEURL, args);

    var track = args.track;
    if (track == null || typeof (track) == 'undefined') {
        track = $.getQueryString({ 'ID': 'track', 'DeafultValue': '' });
    }

    var hidetrack = $.getQueryString({ 'ID': 'hidetrack' });

    if ((track == 1 || 'true'.equals(track)) && !hidetrack) {
        $('#header').find('h1').css('display', '');
        $('#header').find('h1').before('<iframe src="' + trackurl + '" height="35" width="100%" frameborder="1" border="1"></iframe>');
    } else {
        $('#header').css('display', 'none');
    }
}