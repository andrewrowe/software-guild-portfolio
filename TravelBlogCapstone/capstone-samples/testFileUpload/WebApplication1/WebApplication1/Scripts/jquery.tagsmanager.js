var Tags;
(function (Tags) {
    var TagsManagerOptions = (function () {
        function TagsManagerOptions() {
            this.prefilled = null;
            this.CapitalizeFirstLetter = false;
            this.preventSubmitOnEnter = true;
            this.isClearInputOnEsc = true;
            this.typeahead = false;
            this.typeaheadAjaxSource = null;
            this.typeaheadAjaxPolling = false;
            this.typeaheadDelegate = null;
            this.typeaheadOverrides = null;
            this.typeaheadSource = null;
            this.AjaxPush = null;
            this.delimeters = [
                44, 
                188, 
                13, 
                9
            ];
            this.backspace = [
                8
            ];
            this.maxTags = 0;
            this.hiddenTagListName = null;
            this.hiddenTagListId = null;
            this.deleteTagsOnBackspace = true;
            this.tagCloseIcon = 'x';
            this.tagClass = '';
            this.validator = null;
        }
        return TagsManagerOptions;
    })();    
    var TypeaheadOverrides = (function () {
        function TypeaheadOverrides() {
            this.instanceSelectHandler = null;
            this.selectedClass = "selected";
            this.select = null;
            if("typeahead" in jQuery.fn) {
                this.instanceSelectHandler = jQuery.fn.typeahead.Constructor.prototype.select;
                this.select = function (overrides) {
                    this.$menu.find(".active").addClass(overrides.selectedClass);
                    overrides.instanceSelectHandler.apply(this, arguments);
                };
            }
        }
        return TypeaheadOverrides;
    })();    
    var TagsManager = (function () {
        function TagsManager(context, options, tagToManipulate) {
            this.tagToManipulate = "";
            if(context.length < 1) {
                return context;
            }
            this.setContext(context, tagToManipulate);
            this.initialize(options, tagToManipulate);
            return this.processTags();
        }
        TagsManager.prototype.initialize = function (options, tagToManipulate) {
            this.tagManagerOptions = new TagsManagerOptions();
            this.tagManagerOptions.typeaheadOverrides = new TypeaheadOverrides();
            this.setOptions(options);
            if(this.tagManagerOptions.hiddenTagListName === null) {
                this.tagManagerOptions.hiddenTagListName = "hidden-" + this.obj.attr('name');
            }
            this.queuedTag = "";
            this.delimeters = this.tagManagerOptions.delimeters;
            this.backspace = this.tagManagerOptions.backspace;
        };
        TagsManager.prototype.setupTypeahead = function () {
            if(!this.obj.typeahead) {
                return;
            }
            if(this.tagManagerOptions.typeaheadSource && jQuery.isFunction(this.tagManagerOptions.typeaheadSource)) {
                this.obj.typeahead({
                    source: this.tagManagerOptions.typeaheadSource
                });
            } else if(this.tagManagerOptions.typeaheadSource) {
                this.obj.typeahead();
                this.setTypeaheadSource(this.tagManagerOptions.typeaheadSource);
            } else if(this.tagManagerOptions.typeaheadAjaxSource) {
                if(!this.tagManagerOptions.typeaheadAjaxPolling) {
                    this.obj.typeahead();
                    var handler = jQuery.proxy(this.onTypeaheadAjaxSuccess, this);
                    if(typeof (this.tagManagerOptions.typeaheadAjaxSource) == "string") {
                        var serviceUrl = this.tagManagerOptions.typeaheadAjaxSource;
                        jQuery.ajax({
                            cache: false,
                            type: "POST",
                            contentType: "application/json",
                            dataType: "json",
                            url: serviceUrl,
                            data: JSON.stringify({
                                typeahead: ""
                            }),
                            success: function (data) {
                                handler(data, true);
                            }
                        });
                    }
                } else if(this.tagManagerOptions.typeaheadAjaxPolling) {
                    this.obj.typeahead({
                        source: jQuery.proxy(this.ajaxPolling, this)
                    });
                }
            } else if(this.tagManagerOptions.typeaheadDelegate) {
                this.obj.typeahead(this.tagManagerOptions.typeaheadDelegate);
            }
            var data = this.obj.data('typeahead');
            if(data) {
                data.select = jQuery.proxy(this.tagManagerOptions.typeaheadOverrides.select, this.obj.data('typeahead'), this.tagManagerOptions.typeaheadOverrides);
            }
        };
        TagsManager.prototype.onTypeaheadAjaxSuccess = function (data, isSetTypeaheadSource, process) {
            var _tagsmanager = (isSetTypeaheadSource) ? this : null;
            if("d" in data) {
                data = data.d;
            }
            if(data && data.tags) {
                var sourceAjaxArray = [];
                sourceAjaxArray.length = 0;
                jQuery.each(data.tags, function (key, val) {
                    sourceAjaxArray.push(val.tag);
                    if(isSetTypeaheadSource) {
                        _tagsmanager.setTypeaheadSource(sourceAjaxArray);
                    }
                });
                if(jQuery.isFunction(process)) {
                    process(sourceAjaxArray);
                }
            }
        };
        TagsManager.prototype.setTypeaheadSource = function (source) {
            this.obj.data('active', true);
            this.obj.data('typeahead').source = source;
            this.obj.data('active', false);
        };
        TagsManager.prototype.ajaxPolling = function (query, process) {
            var handler = jQuery.proxy(this.onTypeaheadAjaxSuccess, this);
            if(typeof (this.tagManagerOptions.typeaheadAjaxSource) == "string") {
                var serviceUrl = this.tagManagerOptions.typeaheadAjaxSource;
                jQuery.ajax({
                    cache: false,
                    type: "POST",
                    contentType: "application/json",
                    dataType: "json",
                    url: serviceUrl,
                    data: JSON.stringify({
                        typeahead: query
                    }),
                    success: function (data) {
                        handler(data, false, process);
                    }
                });
            }
        };
        TagsManager.prototype.trimTag = function (tag) {
            var txt = jQuery.trim(tag);
            var l = txt.length;
            var t = 0;
            for(var i = l - 1; i >= 0; i--) {
                if(-1 == jQuery.inArray(txt.charCodeAt(i), this.delimeters)) {
                    break;
                }
                t++;
            }
            txt = txt.substring(0, l - t);
            l = txt.length;
            t = 0;
            for(var i = 0; i < l; i++) {
                if(-1 == jQuery.inArray(txt.charCodeAt(i), this.delimeters)) {
                    break;
                }
                t++;
            }
            txt = txt.substring(t, l);
            return txt;
        };
        TagsManager.prototype.popTag = function () {
            var tlis = this.obj.data("tlis");
            var tlid = this.obj.data("tlid");
            if(tlid.length > 0) {
                var tagId = tlid.pop();
                tlis.pop();
                jQuery("#" + this.objName + "_" + tagId).remove();
                this.refreshHiddenTagList();
            }
        };
        TagsManager.prototype.empty = function () {
            var tlis = this.obj.data("tlis");
            var tlid = this.obj.data("tlid");
            while(tlid.length > 0) {
                var tagId = tlid.pop();
                tlis.pop();
                jQuery("#" + this.objName + "_" + tagId).remove();
                this.refreshHiddenTagList();
            }
        };
        TagsManager.prototype.refreshHiddenTagList = function () {
            var tlis = this.obj.data("tlis");
            var lhiddenTagList = this.obj.data("lhiddenTagList");
            if(lhiddenTagList == undefined) {
                return;
            }
            jQuery(lhiddenTagList).val(tlis.join(",")).change();
        };
        TagsManager.prototype.spliceTag = function (tagId, eventData) {
            var tlis = this.obj.data("tlis");
            var tlid = this.obj.data("tlid");
            var p = jQuery.inArray(tagId, tlid);
            if(-1 != p) {
                jQuery("#" + this.objName + "_" + tagId).remove();
                tlis.splice(p, 1);
                tlid.splice(p, 1);
                this.refreshHiddenTagList();
            }
            if(this.tagManagerOptions.maxTags > 0 && tlis.length < this.tagManagerOptions.maxTags) {
                this.obj.show();
            }
        };
        TagsManager.prototype.pushTag = function (tag, objToPush, isValid) {
            if(!tag || (!isValid) || tag.length <= 0) {
                return;
            }
            var _tagsmanager = this;
            if(this.tagManagerOptions.CapitalizeFirstLetter && tag.length > 1) {
                tag = tag.charAt(0).toUpperCase() + tag.slice(1).toLowerCase();
            }
            if(jQuery.isFunction(this.tagManagerOptions.validator)) {
                if(!this.tagManagerOptions.validator(tag)) {
                    return;
                }
            }
            var tlis = this.obj.data("tlis");
            var tlid = this.obj.data("tlid");
            if(this.tagManagerOptions.maxTags > 0 && tlis.length >= this.tagManagerOptions.maxTags) {
                return;
            }
            var alreadyInList = false;
            var tlisLowerCase = tlis.map(function (elem) {
                return elem.toLowerCase();
            });
            var p = jQuery.inArray(tag.toLowerCase(), tlisLowerCase);
            if(-1 != p) {
                alreadyInList = true;
            }
            if(alreadyInList) {
                var pTagId = tlid[p];
                jQuery("#" + this.objName + "_" + pTagId).stop().animate({
                    backgroundColor: this.tagManagerOptions.blinkBGColor_1
                }, 100).animate({
                    backgroundColor: this.tagManagerOptions.blinkBGColor_2
                }, 100).animate({
                    backgroundColor: this.tagManagerOptions.blinkBGColor_1
                }, 100).animate({
                    backgroundColor: this.tagManagerOptions.blinkBGColor_2
                }, 100).animate({
                    backgroundColor: this.tagManagerOptions.blinkBGColor_1
                }, 100).animate({
                    backgroundColor: this.tagManagerOptions.blinkBGColor_2
                }, 100);
            } else {
                var max = Math.max.apply(null, tlid);
                max = max == -Infinity ? 0 : max;
                var tagId = ++max;
                tlis.push(tag);
                tlid.push(tagId);
                if(this.tagManagerOptions.AjaxPush) {
                    jQuery.post(this.tagManagerOptions.AjaxPush, {
                        tag: tag
                    });
                }
                var newTagId = this.objName + '_' + tagId;
                var newTagRemoveId = this.objName + '_Remover_' + tagId;
                var html = '';
                var cl = this.tagManagerOptions.tagClass ? ' ' + this.tagManagerOptions.tagClass : '';
                html += '<span class="myTag' + cl + '" id="' + newTagId + '"><span>' + tag + '&nbsp;&nbsp;</span><a href="#" class="myTagRemover" id="' + newTagRemoveId + '" TagIdToRemove="' + tagId + '" title="Remove">' + this.tagManagerOptions.tagCloseIcon + '</a></span> ';
                if(this.tagManagerOptions.tagsContainer) {
                    jQuery(this.tagManagerOptions.tagsContainer).append(html);
                } else {
                    this.obj.before(html);
                }
                jQuery("#" + newTagRemoveId).on("click", this.obj, function (e) {
                    e.preventDefault();
                    var TagIdToRemove = parseInt(jQuery(this).attr("TagIdToRemove"));
                    _tagsmanager.spliceTag(TagIdToRemove, e.data);
                });
                this.refreshHiddenTagList();
                if(this.tagManagerOptions.maxTags > 0 && tlis.length >= this.tagManagerOptions.maxTags) {
                    this.obj.hide();
                }
            }
            this.obj.val("");
        };
        TagsManager.prototype.setOptions = function (options) {
            if(options) {
                jQuery.extend(true, this.tagManagerOptions, options);
            }
        };
        TagsManager.prototype.setContext = function (context, tagToManipulate) {
            this.obj = context;
            this.objName = this.obj.attr('name').replace(/[^\w]/g, '_');
            this.tagToManipulate = tagToManipulate;
        };
        TagsManager.prototype.processCommand = function (context, command, tagToManipulate) {
            if(context.length > 0) {
                return this.processTags(command, context, tagToManipulate);
            } else {
                return context;
            }
        };
        TagsManager.prototype.processTags = function (command, context, tagToManipulate) {
            var _tagsmanager = this;
            return this.obj.each(function () {
                var tagIsValid = false;
                var isSelectedFromList = false;
                if(typeof command == 'string') {
                    switch(command) {
                        case "empty":
                            _tagsmanager.setContext(context, tagToManipulate);
                            _tagsmanager.empty();
                            break;
                        case "popTag":
                            _tagsmanager.setContext(context, tagToManipulate);
                            _tagsmanager.popTag();
                            break;
                        case "pushTag":
                            _tagsmanager.setContext(context, tagToManipulate);
                            _tagsmanager.pushTag(_tagsmanager.tagToManipulate, null, true);
                            break;
                    }
                    return;
                } else {
                    jQuery(this).data("tagsmanager", jQuery.proxy(_tagsmanager.processCommand, _tagsmanager));
                }
                var tlis = new Array();
                var tlid = new Array();
                _tagsmanager.obj.data("tlis", tlis);
                _tagsmanager.obj.data("tlid", tlid);
                if(_tagsmanager.tagManagerOptions.hiddenTagListId == null) {
                    var hiddenTag = $("input[name=" + _tagsmanager.tagManagerOptions.hiddenTagListName + "]");
                    if(hiddenTag.length > 0) {
                        hiddenTag.remove();
                    }
                    var html = "";
                    html += "<input name='" + _tagsmanager.tagManagerOptions.hiddenTagListName + "' type='hidden' value=''/>";
                    _tagsmanager.obj.after(html);
                    _tagsmanager.obj.data("lhiddenTagList", _tagsmanager.obj.siblings("input[name='" + _tagsmanager.tagManagerOptions.hiddenTagListName + "']")[0]);
                } else {
                    _tagsmanager.obj.data("lhiddenTagList", jQuery('#' + _tagsmanager.tagManagerOptions.hiddenTagListId));
                }
                if(_tagsmanager.tagManagerOptions.typeahead) {
                    _tagsmanager.setupTypeahead();
                }
                _tagsmanager.obj.on("focus", function (e) {
                    if(jQuery(this).popover) {
                        jQuery(this).popover("hide");
                    }
                });
                if(_tagsmanager.tagManagerOptions.isClearInputOnEsc) {
                    _tagsmanager.obj.on("keyup", function (e) {
                        if(e.which == 27) {
                            jQuery(this).val("");
                            e.cancelBubble = true;
                            e.returnValue = false;
                            e.stopPropagation();
                            e.preventDefault();
                        }
                    });
                }
                _tagsmanager.obj.on("keypress", function (e) {
                    if(jQuery(this).popover) {
                        jQuery(this).popover("hide");
                    }
                    if(_tagsmanager.tagManagerOptions.preventSubmitOnEnter) {
                        if(e.which == 13) {
                            e.cancelBubble = true;
                            e.returnValue = false;
                            e.stopPropagation();
                            e.preventDefault();
                        }
                    }
                    var p = jQuery.inArray(e.which, _tagsmanager.delimeters);
                    if(-1 != p) {
                        tagIsValid = true;
                        var user_input = jQuery(this).val();
                        user_input = _tagsmanager.trimTag(user_input);
                        _tagsmanager.pushTag(user_input, e.data, tagIsValid);
                        e.preventDefault();
                    } else {
                        tagIsValid = false;
                    }
                });
                if(_tagsmanager.tagManagerOptions.deleteTagsOnBackspace) {
                    _tagsmanager.obj.on("keydown", _tagsmanager.obj, function (e) {
                        var p = jQuery.inArray(e.which, _tagsmanager.backspace);
                        if(-1 != p) {
                            var user_input = jQuery(this).val();
                            var i = user_input.length;
                            if(i <= 0) {
                                e.preventDefault();
                                _tagsmanager.popTag();
                            }
                        }
                    });
                }
                _tagsmanager.obj.change(function (e) {
                    e.cancelBubble = true;
                    e.returnValue = false;
                    e.stopPropagation();
                    e.preventDefault();
                    var selectedItemClass = _tagsmanager.tagManagerOptions.typeaheadOverrides.selectedClass;
                    var listItemSelector = '.' + selectedItemClass;
                    var data = $(this).data('typeahead');
                    if(data) {
                        isSelectedFromList = $(this).data('typeahead').$menu.find("*").filter(listItemSelector).hasClass(selectedItemClass);
                        if(isSelectedFromList) {
                            tagIsValid = true;
                        }
                    }
                    if(!tagIsValid) {
                        return;
                    }
                    var is_chrome = navigator.userAgent.indexOf('Chrome') > -1;
                    var is_explorer = navigator.userAgent.indexOf('MSIE') > -1;
                    var is_firefox = navigator.userAgent.indexOf('Firefox') > -1;
                    var is_safari = navigator.userAgent.indexOf("Safari") > -1;
                    if(!is_chrome && !is_safari) {
                        jQuery(this).focus();
                    }
                    var ao = jQuery(".typeahead:visible");
                    if(ao[0]) {
                        var isClear = !isSelectedFromList;
                        if(isSelectedFromList) {
                            var user_input = $(this).data('typeahead').$menu.find(listItemSelector).attr('data-value');
                            user_input = _tagsmanager.trimTag(user_input);
                            if(_tagsmanager.queuedTag == jQuery(this).val() && _tagsmanager.queuedTag == user_input) {
                                isClear = true;
                            } else {
                                _tagsmanager.pushTag(user_input, null, true);
                                _tagsmanager.queuedTag = user_input;
                            }
                            isSelectedFromList = false;
                            $(this).data('typeahead').$menu.find(listItemSelector).removeClass(selectedItemClass);
                        }
                        if(isClear) {
                            _tagsmanager.queuedTag = "";
                            jQuery(this).val(_tagsmanager.queuedTag);
                        }
                    } else {
                        var user_input = jQuery(this).val();
                        user_input = _tagsmanager.trimTag(user_input);
                        _tagsmanager.pushTag(user_input, null, true);
                    }
                    tagIsValid = false;
                });
                if(1 == 1 || !_tagsmanager.tagManagerOptions.typeahead) {
                    _tagsmanager.obj.on("blur", function (e) {
                        e.cancelBubble = true;
                        e.returnValue = false;
                        e.stopPropagation();
                        e.preventDefault();
                        var push = true;
                        if(_tagsmanager.tagManagerOptions.typeahead) {
                            var ao = jQuery(".typeahead:visible");
                            if(ao[0]) {
                                push = false;
                            } else {
                                push = true;
                            }
                        }
                        if(push) {
                            var user_input = jQuery(this).val();
                            user_input = _tagsmanager.trimTag(user_input);
                            _tagsmanager.pushTag(user_input, null, tagIsValid);
                        }
                    });
                }
                if(_tagsmanager.tagManagerOptions.prefilled) {
                    if(typeof (_tagsmanager.tagManagerOptions.prefilled) == "object") {
                        var pta = _tagsmanager.tagManagerOptions.prefilled;
                        jQuery.each(pta, function (key, val) {
                            var a = 1;
                            _tagsmanager.pushTag(val, _tagsmanager.obj, true);
                        });
                    } else if(typeof (_tagsmanager.tagManagerOptions.prefilled) == "string") {
                        var pta = _tagsmanager.tagManagerOptions.prefilled.split(',');
                        jQuery.each(pta, function (key, val) {
                            var a = 1;
                            _tagsmanager.pushTag(val, _tagsmanager.obj, true);
                        });
                    }
                }
            });
        };
        return TagsManager;
    })();
    Tags.TagsManager = TagsManager;    
})(Tags || (Tags = {}));
(function (jQuery) {
    jQuery.fn.tagsManager = function (options, tagToManipulate) {
        if(typeof (options) == "string") {
            var result = this.filter("html");
            this.each(function () {
                var obj = jQuery(this);
                var fn = obj.data("tagsmanager");
                var el = fn(obj, options, tagToManipulate);
                result.add(el);
            });
            return result;
        } else {
            return new Tags.TagsManager(this, options, tagToManipulate);
        }
    };
})(jQuery);
