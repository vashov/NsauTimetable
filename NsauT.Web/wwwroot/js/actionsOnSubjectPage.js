function editPeriod(periodId, dayId) {
    var btn = $('#period' + periodId + ' .edit-btn');
    var requestUrl = btn.data('request-url');
    console.log('edit period' + requestUrl);
    console.log(periodId + ' ' + dayId);

    window.location = requestUrl + '/' + periodId + '?dayId=' + dayId;
}

function addNewPeriod(dayId) {
    var btn = $('#day' + dayId + ' .new-period-btn');
    var requestUrl = btn.data('request-url');
    console.log('add new period ' + requestUrl);
    console.log(dayId);

    window.location = requestUrl + '?dayId=' + dayId;
}

function approvePeriod(periodId, dayId) {
    console.log('try approve period ' + periodId + ', day ' + dayId);

    var btn = $('#period' + periodId + ' .approve-btn');
    var requestUrl = btn.data('request-url');
    btn.prop('disabled', true);
    btn.html('<span class="spinner-border spinner-border-sm"></span> Загрузка...');

    $.ajax({
        type: 'PUT',
        url: requestUrl + '/' + periodId + '?dayId=' + dayId,
        dataType: 'json',
        error: function () {
            btn.html('Проверено');
            btn.prop('disabled', false);
            alert('error');
        },
        success: function (result) {
            console.log('success: ' + result);
            console.log('success: ' + result.periodApproved + ' ' + result.dayApproved);

            if (!result.periodApproved) {
                return;
            }

            var periodImg = $('#period' + periodId + ' img');
            var checkImgUrl = periodImg.data('check-img-url');
            periodImg.attr('src', checkImgUrl);
            periodImg.attr('alt', 'Да');

            btn.prop('disabled', true);
            btn.html('Проверено');
            btn.addClass('btn-outline-secondary');
            btn.removeClass('btn-outline-success');

            if (!result.dayApproved) {
                return;
            }

            var dayImg = $('#day' + dayId + ' img');
            dayImg.attr('src', checkImgUrl);
            dayImg.attr('alt', 'Да');
        }
    });
}

function deletePeriod(periodId, dayId) {
    console.log('try delete period ' + periodId);

    var numperOfPeriod = $('#period' + periodId + ' .period-number').text();
    if (!confirm(numperOfPeriod + ' - Удалить пару?')) {
        return;
    }

    var btn = $('#period' + periodId + ' .delete-btn');
    btn.prop('disabled', true);
    btn.html('<span class="spinner-border spinner-border-sm"></span> Удаление...');
    var requestUrl = btn.data('request-url');

    $.ajax({
        type: 'DELETE',
        url: requestUrl + '/' + periodId + '?dayId=' + dayId,
        dataType: 'json',
        error: function () {
            btn.html('Удалить');
            btn.prop('disabled', false);
            alert('error');
        },
        success: function (result) {
            $('#period' + periodId).remove();

            if (!result.dayApproved) {
                return;
            }

            var dayImg = $('#day' + dayId + ' img');
            var checkImgUrl = dayImg.data('check-img-url');
            dayImg.attr('src', checkImgUrl);
            dayImg.attr('alt', 'Да');
        }
    });
}

function deleteDay(dayId) {
    console.log('try delete day ' + dayId);
    var dayName = $('#day' + dayId + ' .dayName').text();
    if (!confirm('(' + dayName + ') - Удалить день?')) {
        return;
    }

    var buttons = $('#day' + dayId + ' button');
    buttons.prop('disabled', true);

    var btnDelete = $('#day' + dayId + ' button.delete-day-btn');
    btnDelete.html('<span class="spinner-border spinner-border-sm"></span> Удаление...');
    var requestUrl = btnDelete.data('request-url');

    $.ajax({
        type: 'DELETE',
        url: requestUrl + '/' + dayId,
        dataType: 'text',
        error: function () {
            btnDelete.html('Удалить день');
            buttons.prop('disabled', false);
            alert('error');
        },
        success: function (result) {
            console.log("success delete day");
            var dayElement = $('#day' + dayId);
            dayElement.removeClass('mb-5');
            dayElement.html('<div class="col-12 bg-light rounded">[' + dayName + '] День удален</div>');
            dayElement.fadeOut(2000);
        }
    });
}